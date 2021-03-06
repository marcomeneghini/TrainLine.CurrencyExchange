## How To run the code
* Clone and compile the repository
* Set the Api project as Startup project
* Run the solution and the Swagger landing page will appear
* To test the GET /api/CurrencyExchange try to set as source and destination EUR / GBP / USD
* set an amount > 0
* In case source or destination are set with different values than the ones provided, a 404 with  managed error will be returned
* In case an amount <= 0, a 400 with  managed error will be returned


## MVP1: create an API that performs Currency Exchange

GET api/currencyexchange?source={sourceCurrency}&destination={destinationCurrency}&amount={amount}

* A currency object must be loaded with remote configuration
*  Use a factory to load it as there are async operations that cannot be added i the constructor
*  the remote configuration can be cached for a short time, from local configuration (temporary TTL managed by controller, not in config)
*  always prefer the Latest configuration from remote source
*  if remote source not available and cached elements too old, throw an error "exchange rate not updated" throws exception is currency not found
*  NTH - perform Validation on CurrencyCode and amount 

# Test Cases
1. Source currency not found (404)
2. Destination currency not found (404)
3. no remote configuration available, no cached configuration (500)
4. no remote configuration available, cached configuration old (500) (same as above as the cache delete the entry after X seconds)
5. remote configuration available (200)
5. no remote configuration available, cached configuration ok (200)


## MVP2 : Decouple Currenncy Exchange from Getting last exchange rate remotely, inprocess
*  Try to avoid throwing exceptions because of performances
*  Catch the remaining exception in a Middleware 
*  Add integration tests (test infrastructure)
*  Verify "rounding" strategy during money exchange
*  Use different exceptions over "httpCode" (the domain should not care about any Http stuff)
*  ExchangeRateDto - EpochToDatetimeConverter / DynamicToDictionaryConverter
*  Event Driven Architecture 
*  *  Create a BGService that query the remote service every minute, 1 call for all the available currencies, send event of CurrencyExchangeUpdated
*  *  CurrencyExchange service subscribes to CurrencyExchangeUpdated and saves last exchange rate on event arrived



## MVP3
*  add authentication/authorization
*  save every exchange 
*  Use CQRS/ES on currencies to keep track of the history of every variation for every currency ( derive Currency entity)
*  add structured log (Serilog + ELK)




