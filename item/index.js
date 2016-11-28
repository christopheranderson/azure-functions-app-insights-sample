var uuid = require('node-uuid');
var appInsights = require("applicationinsights");
var client = appInsights.getClient();

var items = require('../lib/items.js');

exports.run = function(context, req) {
    var start = new Date();
    context.log('Node.js HTTP trigger function processed a request. RequestUri=%s', req.originalUrl);
    req.url = req.originalUrl;
    var operation = uuid.v4();
    client.trackEvent("function.item.execution", 
        {
            operation: operation, 
            query: JSON.stringify(req.query), 
            body: JSON.stringify(req.body ? req.body : {})
        });

    if(req.method == 'GET') {
        items.get((items) => {
            context.res = {
                body: {
                    items: items
                }
            }
            client.trackRequestSync(req, context.res, (new Date() - start));
            context.done();
        })
    } else if (req.method == 'POST') {
        if(req.body && req.body.items) {
            context.bindings.output = [];
            req.body.items.forEach((item) => {
                context.bindings.output.push({
                    item: item,
                    operation: operation
                });  
            })
        }
            context.done();
    } else {
        context.done();
    }
};

