var MenuView = Backbone.View.extend({
    el: '#menu-view',

    events: {
    },
    
    initialize: function (params) {
        if (!(params.tabId | 0)) {
            params.tabId = 1;
        }

        $('#menu-ul li[tabid]', this.el).removeClass('active');
        var activeTab = $('#menu-ul li[tabid=' + params.tabId + ']', this.el);
        activeTab.addClass('active');

        if (!(params.subTabId | 0)) {
            params.subTabId = 1;
        }

        activeTab.find('li[subtabid]', this.el).removeClass('active');
        activeTab.find('li[subtabid=' + params.subTabId + ']', this.el).addClass('active');
    }
});