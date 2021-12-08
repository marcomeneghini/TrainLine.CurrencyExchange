## MVP1: create an API that performs Currency Exchange

GET api/currencyexchange?source={sourceCurrency}&destination={destinationCurrency}&amount={amount}

* A currency object must be loaded with remote configuration
*  Use a factory to load it as there are async operations that cannot be added i the constructor
*  the remote configuration can be cached for a short time, from local configuration (temporary TTL managed by controller, not in config)
*  always prefer the Latest configuration from remote source
*  if remote source not available and cached elements too old, throw an error "exchange rate not updated" throws exception is currency not found
*  perform Validation on CurrencyCode

# Test Cases
1. Source currency not found (404)
2. Destination currency not found (404)
3. no remote configuration available, no cached configuration (500)
4. no remote configuration available, cached configuration old (500) (same as above as the cache delete the entry after X seconds)
5. remote configuration available (200)
5. no remote configuration available, cached configuration ok (200)


## MVP2 : Decouple Currenncy Exchange from Getting last exchange rate remotely, inprocess
*  Add integration tests (test infrastructure)
*  Use different exceptions over "httpCode" (the domain should not care about any Http stuff)
*  ExchangeRateDto - EpochToDatetimeConverter / DynamicToDictionaryConverter
*  Event Driven Architecture 
*  *  Create a BGService that query the remote service every minute, 1 call for all the available currencies, send event of CurrencyExchangeUpdated
*  *  CurrencyExchange service subscribes to CurrencyExchangeUpdated and saves last exchange rate on event arrived


## MVP3
*  save every exchange 
*  Use CQRS/ES on currencies to keep track of the history 

