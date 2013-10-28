var GeochemicalAssessmentView = Backbone.View.extend({
    el: "#view-geochemical-assessment",

    events: {
        "change #RegionId": "loadGrid",
        "change input[type=text], input[type=number]": "calculate"
    },

    initialize: function (params) {
        this.normalizing = params.normalizing;
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
        items.find('.control-group.error').removeClass('error');
        var count = 0;
        var sum = 0;
        items.each(function () {
            var $this = $(this);
            var val1 = parseFloat($this.find('td.value1').text().replace(',', '.')) || 0;
            var val2 = parseFloat($this.find('td.value2 input').val().replace(',', '.')) || 0;
            if (val2 <= 0) {
                $this.find('td.value2 input').closest('.control-group').addClass('error');
            }

            var val = val2 / val1;
            if (val > 1) {
                sum = sum + val;
                count++;
            }
        });

        var result = sum - count + 1;
        $('.index-label', this.el).text(result);
        this.normalize(result);
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
})