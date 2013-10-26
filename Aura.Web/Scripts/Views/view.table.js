var TableView = Backbone.View.extend({
    el: '#table-view',

    events: {
        "click td[colid]": "itemUpdate"
    },

    initialize: function (params) {
        this.tableId = params.tableId;
        this.url = params.url;
    },

    itemUpdate: function (event) {
        var sender = $(event.currentTarget);
        var oldValue = sender.text();
        var value = prompt("Новое значение", oldValue);

        if (!value || value === oldValue) {
            return;
        }

        if (/^[0-9]+(,[0-9]{1,3})?$/ig.test(value)) {
            var model = {
                tableId: this.tableId,
                columnId: sender.attr("colid"),
                regionId: sender.closest('tr').attr('rowid'),
                value: value
            };

            var postUrl = this.url;
            var alterUrl = sender.attr('alter-update-action');
            if (alterUrl) {
                postUrl = alterUrl;
            }

            $.post(postUrl, model, function (result) {
                if (!result.success) {
                    alert(result.message);
                } else {
                    sender.text(value);
                }
            });
        } else {
            alert("Не верное значение");
        }
    }
});
