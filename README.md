# AccountLookupAPI

The `AccountLookupAPI` provides multiple endpoints for performing account and name lookups via POST requests. The API accepts XML-formatted requests and returns XML responses. This API uses static datasets for demonstration and testing purposes.

---

## Endpoints

### 1. /api/SearchByAccountNumber

**Request:**
```xml
<?xml version="1.0" encoding="UTF-8"?>
<ExactLookupRequest>
	<AccountNumber></AccountNumber>
</ExactLookupRequest>
```

**Description:**
- Searches for account details based on the provided `AccountNumber`.
- Returns account information if a match is found.

**Behavior:**
- No return if the provided `AccountNumber` is `"1234567890"`.

---

### 2. /api/SearchByAccountNumberTA

**Request:**
```xml
<?xml version="1.0" encoding="UTF-8"?>
<ExactLookupRequest>
	<AccountNumber></AccountNumber>
</ExactLookupRequest>
```

**Description:**
- Similar to `/api/SearchByAccountNumber`, this endpoint performs a targeted account lookup.
- Specifically designed for static data validation and testing purposes.

**Behavior:**
- Returns a predefined response for static datasets.
- Account number '1234567890' will yield no results.

---

### 3. /api/SearchByName

**Request:**
```xml
<?xml version="1.0" encoding="UTF-8"?>
<WildLookupRequest>
	<FirstName></FirstName>
	<LastName></LastName>
	<City></City>
	<State></State>
	<Zip></Zip>
</WildLookupRequest>
```

**Description:**
- Performs a wildcard search for accounts based on the provided name and address criteria.
- Returns results that match any combination of the given fields (`FirstName`, `LastName`, `City`, `State`, `Zip`).

**Behavior:**
- Searches across a static dataset containing 5 predefined entries.
- **Example Search Values:**
  - `FirstName = John`
  - `LastName = Doe`

---

### 4. /api/SearchByNameTA

**Request:**
```xml
<?xml version="1.0" encoding="UTF-8"?>
<WildLookupRequest>
	<FirstName></FirstName>
	<LastName></LastName>
	<City></City>
	<State></State>
	<Zip></Zip>
</WildLookupRequest>
```

**Description:**
- Similar to `/api/SearchByName`, but tailored for targeted searches in predefined datasets.
- Designed for testing and static data validation.

**Behavior:**
- Searches through a static dataset with up to 5 predefined records.
- **Possible Search Values:**
  - `FirstName = John`
  - `LastName = Doe`

---

## Example Requests and Responses

### Example: /api/SearchByName

**Request:**
```xml
<?xml version="1.0" encoding="UTF-8"?>
<WildLookupRequest>
	<FirstName>John</FirstName>
	<LastName>Doe</LastName>
	<City>New York</City>
	<State>NY</State>
	<Zip>10001</Zip>
</WildLookupRequest>
```

**Response:**
```xml
<?xml version="1.0" encoding="UTF-8"?>
<Root>
	<LookupResponse>
		<LookupResults>
			<LookupResult>
				<FirstName>John</FirstName>
				<LastName>Doe</LastName>
				<City>New York</City>
				<State>NY</State>
				<Zip>10001</Zip>
			</LookupResult>
		</LookupResults>
	</LookupResponse>
</Root>
```

---

### Example: /api/SearchByAccountNumber

**Request:**
```xml
<?xml version="1.0" encoding="UTF-8"?>
<ExactLookupRequest>
	<AccountNumber>9876543210</AccountNumber>
</ExactLookupRequest>
```

**Response:**
```xml
<?xml version="1.0" encoding="UTF-8"?>
<Root>
	<LookupResponse>
		<LookupResults>
			<LookupResult>
				<AccountNumber>9876543210</AccountNumber>
			</LookupResult>
		</LookupResults>
	</LookupResponse>
</Root>
