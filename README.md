# Epinova.ArvatoPaymentGateway
Epinova's take on Arvato's AfterPay payemnt gateway API

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Epinova.ArvatoPaymentGateway&metric=alert_status)](https://sonarcloud.io/dashboard?id=Epinova.ArvatoPaymentGateway)
[![Build status](https://ci.appveyor.com/api/projects/status/0tkmpwvxrbnlpqmx/branch/master?svg=true)](https://ci.appveyor.com/project/Epinova_AppVeyor_Team/epinova-arvatopaymentgateway/branch/master)
[![GitHub version](https://badge.fury.io/gh/Epinova%2FEpinova.ArvatoPaymentGateway.svg)](https://github.com/Epinova/Epinova.ArvatoPaymentGateway)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Usage
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