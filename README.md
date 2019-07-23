# Epinova.ArvatoPaymentGateway
Epinova's take on Arvato's AfterPay payment gateway API. All about invoices and down payment plans.

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Epinova.ArvatoPaymentGateway&metric=alert_status)](https://sonarcloud.io/dashboard?id=Epinova.ArvatoPaymentGateway)
[![Build status](https://ci.appveyor.com/api/projects/status/0tkmpwvxrbnlpqmx/branch/master?svg=true)](https://ci.appveyor.com/project/Epinova_AppVeyor_Team/epinova-arvatopaymentgateway/branch/master)
![Tests](https://img.shields.io/appveyor/tests/Epinova_AppVeyor_Team/epinova-arvatopaymentgateway.svg)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Getting Started

### Configuration

No configuration via config files are needed, but you can set up different API endpoint addresses for different environments via an appSetting.

web.config:
```xml
<configuration>
    <appSettings>
      <!-- Test environment: -->
      <add key="AfterPay.Api.BaseAddress" value="https://sandboxapi.horizonafs.com/eCommerceServicesWebApi/" />
      <!-- Production environment: -->
      <add key="AfterPay.Api.BaseAddress" value="https://api.afterpay.io/" />
    <appSettings>
</configuration>
```

If not provided the production URL is used by default.

### Add registry to IoC container

If using Structuremap:
```csharp
container.Configure(
    x =>
    {
        x.Scan(y =>
        {
            y.TheCallingAssembly();
            y.WithDefaultConventions();
        });

        x.AddRegistry<Epinova.ArvatoPaymentGateway.InvoiceGatewayRegistry>();
    });
```

If you cannot use the [structuremap registry](src/InvoiceGatewayRegistry.cs) provided with this module,
you can manually set up [InvoiceGatewayService](src/InvoiceGatewayService.cs) for [IInvoiceGatewayService](src/IInvoiceGatewayService.cs)
& [ICustomerLookupService](src/ICustomerLookupService.cs).

### Inject contract and use service

[Epinova.ArvatoPaymentGateway.IInvoiceGatewayService](src/IInvoiceGatewayService.cs) describes the main invoice gateway service.
[Epinova.ArvatoPaymentGateway.ICustomerLookupService](src/ICustomerLookupService.cs) describes the customer lookup service.

### Prerequisites

* [EPiServer.Framework](http://www.episerver.com/web-content-management) >= v11.1 for logging purposes.
* [Automapper](https://github.com/AutoMapper/AutoMapper) >= v8.1 for mapping models.
* [StructureMap](http://structuremap.github.io/) >= v4.7 for registering service contract.

### Installing

The module is published on nuget.org.

```bat
nuget install Epinova.ArvatoPaymentGateway
```

## Built With

* .Net Framework 4.6.2

## Authors

* **Tarjei Olsen** - *Initial work* - [apeneve](https://github.com/apeneve)

See also the list of [contributors](https://github.com/Epinova/Epinova.ArvatoPaymentGateway/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## Further reading

[AfterPay API documentation](https://developer.afterpay.io/api)