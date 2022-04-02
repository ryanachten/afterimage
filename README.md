# afterimage

## Development
Running locally, using the `afterimage.Server` launch profile in Visual Studio, you will need a `afterimage` AWS profile created.

For example, in `.aws/config`
```
[profile afterimage]
output         = json
region         = *BUCKET REGION*
```
And in `.aws/credentials`
```
[afterimage]
aws_access_key_id = *ACCESS KEY FROM AWS*
aws_secret_access_key = *SECRET ACCESS KEY FROM AWS*
```