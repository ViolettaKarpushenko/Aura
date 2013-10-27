var GeochemicalAssessmentView = Backbone.View.extend({
    el: "#view-geochemical-assessment",

    events: {
        "change #RegionId": "loadGrid",
        "change input[type=text], input[type=number]": "calculate"
    },

    normalizing: [
        { min: 0, max: 10, value: 1, color: "success" },
        { min: 10, max: 20, value: 2, color: "warning" },
        { min: 20, max: 30, value: 3, color: "warning" },
        { min: 40, max: 70, value: 4, color: "warning" },
        { min: 70, max: 1e+15, value: 5, color: "danger" }
    ],

    initialize: function (params) {
        this.fetchUrl = params.fetchUrl;
        this.loadGrid();
    },

    loadGrid: function () {
        var regionId = $('#RegionId', this.el).val();
        $.get(this.fetchUrl, { regionId: regionId }, $.proxy(function (response) {
            $('.grid-container', this.el).empty().append(response);
            this.calculate();
        }, this));
    },

    calculate: function () {
        var items = $('table tbody tr', this.el);
        var count = 0;
        var sum = 0;
        items.each(function () {
            var $this = $(this);
            var val1 = parseFloat($this.find('td.value1').text());
            var val2 = parseFloat($this.find('td.value2 input').val());
            var val = val2 / val1;
            if (val > 1) {
                sum = sum + val;
                count++;
            }
        });

        var result = sum - count + 1;
        $('.index-label', this.el).text(result);

        var ball = _.find(this.normalizing, function (item) { return item.min <= result && item.max > result; });
        $('.ball-label', this.el).text(ball.value);
        var bar = $('.progress', this.el);
        bar.removeClass('progress-success').removeClass('progress-warning').removeClass('progress-danger').addClass('progress-' + ball.color);
        bar.find('.bar').css('width', (ball.value * 20) + '%');
    }
})