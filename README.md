# AccountLookupAPI

via POST

## /api/SearchByAccountNumber
<?xml version="1.0" encoding="UTF-8"?>
<ExactLookupRequest>
	<AccountNumber></AccountNumber>
</ExactLookupRequest>

## /api/SearchByAccountNumberTA
<?xml version="1.0" encoding="UTF-8"?>
<ExactLookupRequest>
	<AccountNumber></AccountNumber>
</ExactLookupRequest>

Static return
No return if AccountNumber = "1234567890"

## /api/SearchByName
<?xml version="1.0" encoding="UTF-8"?>
<WildLookupRequest>
	<FirstName></FirstName>
	<LastName></LastName>
	<City></City>
	<State></State>
	<Zip></Zip>
</WildLookupRequest>

## /api/SearchByNameTA
<?xml version="1.0" encoding="UTF-8"?>
<WildLookupRequest>
	<FirstName></FirstName>
	<LastName></LastName>
	<City></City>
	<State></State>
	<Zip></Zip>
</WildLookupRequest>

Static dataset (len 5)
Possible search values: FirstName = John, LastName = Doe
