var RegionsView = Backbone.View.extend({
    el: "#regions-view",

    events: {
        "click .edit-region": "editRegion",
        "click .cancel-save-region": "cancelSaveRegion",
        "click button.add-region": "openAddRegion",
        "click .cancel-add-region": "cancelAddRegion",
        "click i.add-region": "addRegion",
        "click i.save-region": "saveRegion",
        "click i.delete-region": "deleteRegion"
    },

    initialize: function (params) {
        this.addUrl = params.addUrl;
        this.updateUrl = params.updateUrl;
        this.deleteUrl = params.deleteUrl;
    },

    editRegion: function (event) {
        var row = $(event.currentTarget).closest('tr');

        $('span.region-name, i.edit-region, i.delete-region', row).hide();
        $('input.region-name', row).show();
        $('i.save-region, i.cancel-save-region', row).show().css('display', 'inline-block');
    },

    cancelSaveRegion: function (event) {
        var row = $(event.currentTarget).closest('tr');

        var nameSpan = $('span.region-name', row);
        var nameInput = $('input.region-name', row);

        if (nameSpan.text() === nameInput.val() || (nameSpan.text() !== nameInput.val() && confirm("Уверены что хотите отменить изменения?"))) {
            nameInput.val(nameSpan.text());

            $('input.region-name, i.save-region, i.cancel-save-region', row).hide();
            $('span.region-name', row).show();
            $('i.edit-region, i.delete-region', row).show().css('display', 'inline-block');
            $('span.help-inline', row).remove();
        }
    },

    openAddRegion: function (event) {
        var row = $('tr[row-id=null]', this.el);
        $(event.currentTarget).hide();
        row.show();
    },

    cancelAddRegion: function () {
        var row = $('tr[row-id=null]', this.el);

        var nameInput = $('input.new-region-name', row);

        if (nameInput.val() === '' || (nameInput.val() !== '' && confirm("Уверены что хотите отменить изменения?"))) {
            nameInput.val('');
            row.hide();
            $('span.help-inline', row).remove();
            $('button.add-region', this.el).show();
            $('span.help-inline', row).remove();
        }
    },

    addRegion: function () {
        var row = $('tr[row-id=null]', this.el);

        $('span.help-inline', row).remove();

        var nameInput = $('input.new-region-name');
        if (nameInput.val() === '') {
            nameInput.after('<span class="help-inline">Название области не может быть пустым.</span>');
            return;
        }

        if (nameInput.val().length > 50) {
            nameInput.after('<span class="help-inline">Название области не может быть длиннее 50 символов.</span>');
            return;
        }

        if (!/^[А-Яа-я ]+$/ig.test(nameInput.val())) {
            nameInput.after('<span class="help-inline">Название области не может содержать символы отличные от русских букв и пробелов.</span>');
            return;
        }

        var model = {
            name: nameInput.val()
        };

        $.post(this.addUrl, model, $.proxy(function (response) {
            if (response.success === true) {
                window.location.reload();
                return;
            }

            if (!response.message) {
                response.message = "Сервис временно не доступен.";
            }

            if (response.selector) {
                $(response.selector, row).after('<span class="help-inline">' + response.message + '</span>');
            } else {
                alert(response.message);
            }
        }, this));
    },

    saveRegion: function (event) {
        var row = $(event.currentTarget).closest('tr');

        $('span.help-inline', this.el).remove();

        var nameSpan = $('span.region-name', row);
        var nameInput = $('input.region-name', row);

        if (nameSpan.text() === nameInput.val()) {
            $('input.region-name, i.save-region, i.cancel-save-region', row).hide();
            $('span.region-name', row).show();
            $('i.edit-region, i.delete-region', row).show().css('display', 'inline-block');
            return;
        }

        if (nameInput.val() === '') {
            nameInput.after('<span class="help-inline">Название области не может быть пустым.</span>');
            return;
        }

        if (nameInput.val().length > 50) {
            nameInput.after('<span class="help-inline">Название области не может быть длиннее 50 символов.</span>');
            return;
        }

        if (!/^[А-Яа-я ]+$/ig.test(nameInput.val())) {
            nameInput.after('<span class="help-inline">Название области не может содержать символы отличные от русских букв и пробелов.</span>');
            return;
        }


        var model = {
            id: row.attr('row-id'),
            name: nameInput.val()
        };

        $.post(this.updateUrl, model, $.proxy(function (response) {
            if (response.success === true) {
                nameSpan.text(nameInput.val());

                $('input.region-name, i.save-region, i.cancel-save-region', row).hide();
                $('span.region-name', row).show();
                $('i.edit-region, i.delete-region', row).show().css('display', 'inline-block');
                return;
            }

            if (!response.message) {
                response.message = "Сервис временно не доступен.";
            }

            if (response.selector) {
                $(response.selector, row).after('<span class="help-inline">' + response.message + '</span>');
            } else {
                alert(response.message);
            }
        }, this));
    },

    deleteRegion: function (event) {
        var row = $(event.currentTarget).closest('tr');

        var model = {
            id: row.attr('row-id')
        };

        $.post(this.deleteUrl, model, $.proxy(function (response) {
            if (response.success === true) {
                row.remove();
                return;
            }

            if (!response.message) {
                response.message = "Сервис временно не доступен.";
            }

            if (response.selector) {
                $(response.selector, row).after('<span class="help-inline">' + response.message + '</span>');
            } else {
                alert(response.message);
            }
        }, this));
    }
});