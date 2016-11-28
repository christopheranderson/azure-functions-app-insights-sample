# Azure Functions with Application Insights

This repo has a few examples of using Application Insights in Node.JS and C#

## Configuring Application Insights

To use these samples, you must set an environment variable for `APPINSIGHTS_INSTRUMENTATIONKEY` (via your Function App's app settings). You should avoid including the instrumentation key directly in your code.

## Node.JS

The `item` and `process-item` functions are Node.JS based and have examples of how to use the SDK. 

You must have the package.json at the root of your project and the node_modules installed (either via direct npm install with Kudu, or via a Git Deployment).

See the Application Insights docs for full usage. https://docs.microsoft.com/en-us/azure/application-insights/app-insights-nodejs

## C\#

The `cs-http` functions are C# based and have examples of how to use the SDK.

You have to have the project.json at the function level. The host will auto fetch the nuget packages for you on compliation. 

See the Application Insights docs for full usage. https://docs.microsoft.com/en-us/azure/application-insights/app-insights-api-custom-events-metrics

## License

[MIT](LICENSE)