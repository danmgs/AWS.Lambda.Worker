REM **configure your profile if required (default profile is named default):
REM aws configure --profile xxxprofilenamexxx

REM **we create the cloudformation template
aws cloudformation create-stack --stack-name my-aws-worker-stack --template-body file://lambda-xray.yaml --parameters file://parameters.json --profile default --region eu-west-3 --capabilities CAPABILITY_IAM

pause

# some options:
# [--disable-rollback | --no-disable-rollback]
# [--rollback-configuration <value>]
# [--timeout-in-minutes <value>]
# [--notification-arns <value>]
# [--capabilities <value>]
# [--resource-types <value>]
# [--role-arn <value>]
# [--on-failure <value>]
# [--stack-policy-body <value>]
# [--stack-policy-url <value>]
# [--tags <value>]
# [--client-request-token <value>]
# [--enable-termination-protection | --no-enable-termination-protection]
# [--cli-input-json <value>]
# [--generate-cli-skeleton <value>]
