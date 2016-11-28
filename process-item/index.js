var appInsights = require("applicationinsights");
var client = appInsights.getClient();

var items = require('../lib/items.js');

module.exports = function (context, item) {
    context.log('Node.js queue trigger function processed work item', item.operation);

    client.trackEvent("function.process-item.execution", 
        {
            operation: item.operation, 
            query: JSON.stringify(item.item), 
        });

    items.add(item.item, () => {
        context.done();
    });
};