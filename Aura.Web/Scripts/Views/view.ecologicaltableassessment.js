var EcologicalTableAssessmentView = Backbone.View.extend({
    el: "#view-geochemical-assessment",

    events: {
        "change #region-id": "loadGrid",
        "change input[type=text], input[type=number]": "changeValue"
    },

    initialize: function (params) {
        this.normalizing = params.normalizing;

        var calculateMethod = $(this.el).attr('calculate');
        if (calculateMethod === 'simple') {
            this.calculate = this.simpleCalculate;
        } else if (calculateMethod === 'extend') {
            this.calculate = this.extendCalculate;
        }

        if (params.fetchUrl) {
            this.fetchUrl = params.fetchUrl;
            this.loadGrid();
        } else {
            this.calculate();
        }
    },

    loadGrid: function () {
        var regionId = $('#region-id', this.el).val();
        $.get(this.fetchUrl, { regionId: regionId }, $.proxy(function (response) {
            $('.grid-container', this.el).empty().append(response);
            this.calculate();
        }, this));
    },

    changeValue: function () {
        this.calculate();
    },

    simpleCalculate: function () {
        var items = $('table tbody tr', this.el);
        items.find('.control-group.error').removeClass('error');
        var sum1 = 0;
        var sum2 = 0;
        items.each(function () {
            var $this = $(this);
            var val1 = parseFloat($this.find('td.value1').text().replace(',', '.')) || 0;
            var val2 = parseFloat($this.find('td.value2 input').val().replace(',', '.')) || 0;
            if (val2 <= 0) {
                $this.find('td.value2 input').closest('.control-group').addClass('error');
            }

            sum1 = sum1 + (val1 * val2);
            sum2 = sum2 + val2;
        });

        var result = sum1 / sum2;
        $('.index-label', this.el).text(result);
        this.normalize(result);
    },

    extendCalculate: function () {
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
            var bar1 = $('.progress', this.el);
            bar1.find('.bar').css('width', '0%');
        } else {
            $('.ball-label', this.el).text(ball.text ? ball.text : ball.value);
            var bar2 = $('.progress', this.el);
            bar2.removeClass('progress-success').removeClass('progress-warning').removeClass('progress-danger').addClass('progress-' + ball.color);
            bar2.find('.bar').css('width', (ball.value * (100 / this.normalizing.length)) + '%');
        }
    }
})