# Epinova.ArvatoPaymentGateway
Epinova's take on Arvato's AfterPay payemnt gateway API

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Epinova.ArvatoPaymentGateway&metric=alert_status)](https://sonarcloud.io/dashboard?id=Epinova.ArvatoPaymentGateway)
[![Build status](https://ci.appveyor.com/api/projects/status/0tkmpwvxrbnlpqmx/branch/master?svg=true)](https://ci.appveyor.com/project/Epinova_AppVeyor_Team/epinova-arvatopaymentgateway/branch/master)
![Tests](https://img.shields.io/appveyor/tests/Epinova_AppVeyor_Team/epinova-arvatopaymentgateway.svg)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Getting Started

### Add registry to Structuremap

```
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

### Inject contract and use service

Epinova.ArvatoPaymentGateway.IInvoiceGatewayService describes the service. 

### Prerequisites

* [EPiServer.Framework](http://www.episerver.com/web-content-management) >= v11.1 for logging purposes.
* [Automapper](https://github.com/AutoMapper/AutoMapper) >= v8.1 for mapping models.
* [StructureMap](http://structuremap.github.io/) >= v4.7 for registering service contract.

### Installing

The module is published on nuget.org.

```
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