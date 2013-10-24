function initMenu($, tabId, subTabId) {
    $(function () {
        if (!(tabId | 0)) {
            tabId = 1;
        }

        $('#menu-ul li[tabid]').removeClass('active');
        var activeTab = $('#menu-ul li[tabid=' + tabId + ']');
        activeTab.addClass('active');

        if (!(subTabId | 0)) {
            subTabId = 1;
        }

        activeTab.find('li[subtabid]').removeClass('active');
        activeTab.find('li[subtabid=' + subTabId + ']').addClass('active');
    });
}

function initItemUpdate($, tableId, url) {
    $(function () {
        $('td[colid]').click(function (event) {
            var sender = $(event.currentTarget);
            var oldValue = sender.text();
            var value = prompt("Новое значение", oldValue);

            if (!value || value === oldValue) {
                return;
            }

            if (/^[0-9]+(,[0-9]{1,3})?$/ig.test(value)) {
                var model = {
                    tableId: tableId,
                    columnId: sender.attr("colid"),
                    regionId: sender.closest('tr').attr('rowid'),
                    value: value
                };

                var postUrl = url;
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
        });
    });
}
