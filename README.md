# ImporterTest

Use any libraries you think is necessary to make `UserImporter` work and to get the
`VerificationTests#Importer_should_work` test to pass.

Valid users should be added to `UserImporterResults.Users`.

Invalid rows should be added to `UserImporterResults.SkippedRows`.


## About the data file

The test data file is a CSV file located at `Test\Verification.csv`.

Strings may be double-quote delimited `"`, single-quote delimited `'`, or not delimited.

### File columns

* First name
  * Cannot be empty
* Last name
  * Cannot be empty
* Email
  * Cannot be empty
* Gender
  * Must be a valid `Gender`
  * Lowercase and uppercase values should be valid
* Favorite number
  * Must be a 32-bit integer
* Join date
  * Format: `MM/DD/YYYY`
  * Must be a valid date