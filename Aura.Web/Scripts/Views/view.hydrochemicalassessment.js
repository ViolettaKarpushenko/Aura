var EcologicalAssessmentView = Backbone.View.extend({
    el: "#view-hydrochemical-assessment",

    events: {
        "click #element-calculator>.placeholder .item .close": "removeItem",
        "change .item>input[type=number]": "calculate",
        "click #clear-btn": "clear"
    },

    initialize: function () {
        $("#element-container>.placeholder>.item", this.el).draggable({
            appendTo: "body",
            helper: "clone"
        });

        var calculatorContainer = $('#element-calculator>.placeholder', this.el);
        calculatorContainer.droppable({
            accept: "#element-container>.placeholder>.item",
            drop: $.proxy(function (event, ui) {
                var item = ui.draggable.outerHTML();

                $(item)
                    .prepend('<button type="button" class="close" data-dismiss="alert">&times;</button>')
                    .append('<br /><input type="number" value="0"/>')
                    .appendTo(calculatorContainer)
                    .fadeIn();

                this.calculate();
            }, this)
        });
    },

    calculate: function () {
        var sum = 0;
        var items = $('#element-calculator>.placeholder>.item', this.el);
        items.each(function () {
            var $this = $(this);
            sum = sum + ($this.find('input[type=number]').val() | 0) / ($this.find('.value').text() | 0);
        });

        sum = sum / items.length;
        $('#index-label', this.el).text(sum);
    },
    
    clear: function() {
        $('#element-calculator>.placeholder>.item', this.el).remove();
        $('#index-label', this.el).text(0);
    },

    removeItem: function () {
        this.calculate();
    }
});
