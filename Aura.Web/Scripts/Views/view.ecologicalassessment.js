var EcologicalAssessmentView = Backbone.View.extend({
    events: {
        "click .item .close": "removeItem",
        "change .item input[type=text]": "changeValculate",
        "click .btn-clear": "clear"
    },

    initialize: function (params) {
        this.normalizing = params.normalizing;

        var draggableItems = $(".element-container-placeholder .item", this.el);
        draggableItems.draggable({
            appendTo: "body",
            helper: "clone"
        });

        var calculateMethod = $(this.el).attr('calculate');
        if (calculateMethod === 'simple') {
            this.calculate = this.simpleCalculate;
        } else if (calculateMethod === 'extend') {
            this.calculate = this.extendCalculate;
        }

        var calculatorContainer = $('.element-calculator-placeholder', this.el);
        calculatorContainer.droppable({
            accept: draggableItems,//".element-container-placeholder .item",
            drop: $.proxy(function (event, ui) {
                var item = ui.draggable.outerHTML();

                $(item)
                    .prepend('<button type="button" class="close" data-dismiss="alert">&times;</button>')
                    .append('<br /><input type="text" value="0"/>')
                    .appendTo(calculatorContainer)
                    .fadeIn();

                // ReSharper disable MisuseOfOwnerFunctionThis
                this.calculate();
                // ReSharper restore MisuseOfOwnerFunctionThis
            }, this)
        });
    },

    changeValculate: function () {
        this.calculate();
    },

    simpleCalculate: function () {
        var sum = 0;
        var items = $('.element-calculator-placeholder .item', this.el);

        items.each(function () {
            var $this = $(this).removeClass('error');
            var value = (parseFloat($this.find('input[type=text]').val()) || 0);
            if (value === 0) {
                $this.addClass('error');
            }

            sum = sum + value / (parseFloat($this.find('.value').text()) || 0);
        });

        var result = sum / items.length;
        $('.element-calculator .index-label', this.el).text(result);
        this.normalize(result);
    },

    extendCalculate: function () {
        var sum1 = 0;
        var sum2 = 0;

        var items = $('.element-calculator-placeholder .item', this.el);
        items.each(function () {
            var $this = $(this).removeClass('error');
            var val = (parseFloat($this.find('input[type=text]').val()) || 0);
            if (val === 0) {
                $this.addClass('error');
            }

            sum1 = sum1 + val * (parseFloat($this.find('.value').text()) || 0);
            sum2 = sum2 + val;
        });

        var result = sum1 / sum2;
        $('.element-calculator .index-label', this.el).text(result);
        this.normalize(result);
    },

    clear: function () {
        $('.element-calculator-placeholder .item', this.el).remove();
        $('.element-calculator .index-label', this.el).text(0);
    },

    removeItem: function (event) {
        $(event.currentTarget).closest('.item').remove();
        this.calculate();
    },

    normalize: function (result) {
        var ball = _.find(this.normalizing, function (item) {
            return item.min <= result && item.max > result;
        });

        if (!ball) {
            $('.ball-label', this.el).text('--');
            var bar = $('.progress', this.el);
            bar.find('.bar').css('width', '0%');
        } else {
            $('.ball-label', this.el).text(ball.value);
            var bar = $('.progress', this.el);
            bar.removeClass('progress-success').removeClass('progress-warning').removeClass('progress-danger').addClass('progress-' + ball.color);
            bar.find('.bar').css('width', (ball.value * (100 / this.normalizing.length)) + '%');
        }
    }
});
