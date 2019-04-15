# Epinova.ArvatoPaymentGateway
Epinova's take on Arvato's AfterPay payemnt gateway API


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