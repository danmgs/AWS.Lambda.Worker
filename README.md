# AWS - LAMBDA - WORKER

An AWS Lambda Worker that is triggered by SNS Topic in order to index message to an Elastic Search Service.

![alt capture](https://github.com/danmgs/AWS.Lambda.Worker/blob/master/img/deployed1.PNG)

## <span style="color:green">Build the lambda zip file and upload to S3</span>

First, click on the file package.bat to generate AWS.Lambda.Worker.zip and upload it to the S3 bucket (***must be global unique name***).
Here I chose mine, by instance:

```
com.aws.lambda.worker.cloudformation
```

![alt capture](https://github.com/danmgs/AWS.Lambda.Worker/blob/master/img/deployartifact1.PNG)

## <span style="color:green">Deploy the setup to AWS</span>

### Deploy with the cloud formation template

You need to upload the template and fill the parameters directly in the AWS console.

```
lambda-xray.yaml
```
This template will :
- retrieve the Lambda zip artifact to deploy it to AWS Lambda Service.
- create the Lambda Role with CloudWatch + X-Ray + Elastic Service permissions.
- create the SNS Topic.

### Deploy with the bat file

You need to edit this bat file according to your region (***--region*** parameter) :
```
cloudformation/
  |
  |->aws-cli-deploy.bat
```

You need to fill the cloud formation parameters in the file :

```
cloudformation/
  |
  |->parameters.json
```

Customize with your own values :
- your S3 bucket (must change to be global and unique name !)
- your elastic search service url
- your SNS Topic
- ..etc

![alt capture](https://github.com/danmgs/AWS.Lambda.Worker/blob/master/img/publishtoaws0.PNG)


These parameters will be passed to the cloud formation template for deployment when clicking the bat file.

## <span style="color:green">Deploy only the lambda worker artifact to AWS</span>

This section details a way to upload the lambda worker manually.<br/>
This will not create the whole setup (create SNS topic, create Lambda Role ..etc).

### Publish via Visual Studio

On the project, right click to publish:

![alt capture](https://github.com/danmgs/AWS.Lambda.Worker/blob/master/img/publishtoaws1.PNG)

![alt capture](https://github.com/danmgs/AWS.Lambda.Worker/blob/master/img/publishtoaws2.PNG)

Fill the options. Please note that these values will edit the file ***aws-lambda-tools-defaults.json*** accordingly.


![alt capture](https://github.com/danmgs/AWS.Lambda.Worker/blob/master/img/publishtoaws3.PNG)

```
xxxaccountidxxx           -> will be your AWS account ID
xxxelasticserviceurlxxx   -> will be your elastic search service url (environment variable value)
```

## <span style="color:green">Testing the application</span>

### Publish message in SNS

In order to trigger the lambda function, publish a message using the generated SNS topic, using the following json structure:

```
	{
		"Title": "Welcome message sample",
		"Content": "Hello world !"
	}
```

### Checking the results

- You can check logs in AWS CloudWatch.
- You can trace and analyze with X-Ray.
- You check the message being indexed under Elastic Seach Service (using Kibana by instance).

## <span style="color:green">Details on the lambda function implementation</span>

Please note the lambda function is implemented in ***dotnetcore2.1*** as AWS manages this version.<br/>

The lambda function ...
- is triggered by the SNS Topic when a message is published.
- indexed the message to an elastic search service. The url of this service is passed as the environment variable ***ESurl***.<br/>
When the environment variable is not existing, there is a fallback configuration (see file ***appsettings.json***).<br/>
But this is not recommended as it is not convenient to configured this way.

## <span style="color:green">Useful links</span>

- [Add an IAM ROLE for the Lambda to execute actions on AWS ElasticSearch service](https://stackoverflow.com/questions/53365764/cloudformation-template-to-push-cloudwatch-logs-to-elasticsearch)
