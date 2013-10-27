var EcologicalAssessmentView = Backbone.View.extend({
    events: {
        "click .item .close": "removeItem",
        "change .item input[type=number]": "changeValculate",
        "click .btn-clear": "clear"
    },

    initialize: function () {
        console.log('initialize');
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
                    .append('<br /><input type="number" value="0"/>')
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
            var $this = $(this);
            sum = sum + ($this.find('input[type=number]').val() | 0) / ($this.find('.value').text() | 0);
        });

        sum = sum / items.length;
        $('.element-calculator .index-label', this.el).text(sum);
    },

    extendCalculate: function () {
        var sum1 = 0;
        var sum2 = 0;
        
        var items = $('.element-calculator-placeholder .item', this.el);
        items.each(function () {
            var $this = $(this);
            sum1 = sum1 + ($this.find('input[type=number]').val() | 0) * ($this.find('.value').text() | 0);
            sum2 = sum2 + ($this.find('input[type=number]').val() | 0);
        });

        var result = sum1 / sum2;
        $('.element-calculator .index-label', this.el).text(result);
    },

    clear: function () {
        $('.element-calculator-placeholder .item', this.el).remove();
        $('.element-calculator .index-label', this.el).text(0);
    },

    removeItem: function (event) {
        $(event.currentTarget).closest('.item').remove();
        this.calculate();
    }
});
