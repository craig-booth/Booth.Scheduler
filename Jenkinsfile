pipeline {
    agent { docker 'mcr.microsoft.com/dotnet/core/sdk:3.0' }
	
	environment {
		PROJECT      = './Booth.Scheduler/Booth.Scheduler.csproj'
        TEST_PROJECT = './Booth.Scheduler.Test/Booth.Scheduler.Test.csproj'

		NUGET_KEY = credentials('nuget')
    }

    stages {
	    stage('Build') {
			steps {
				sh "dotnet build ${PROJECT} --configuration Release"
            }
		}
	    stage('Test') {
			steps {
				sh "dotnet test ${TEST_PROJECT} --configuration Release --logger trx --results-directory ./testresults"
            }
			post {
				always {
					xunit (
						thresholds: [ skipped(failureThreshold: '0'), failed(failureThreshold: '0') ],
						tools: [ MSTest(pattern: 'testresults/*.trx') ]
						)
				}
			}
        }
        stage('Deploy') {
			steps {
				sh "dotnet pack ${PROJECT} --configuration Release --output ./app"
				sh "dotnet nuget push ./app/*.nupkg -k ${NUGET_KEY} -s https://api.nuget.org/v3/index.json"
            }
		}
    }
	
	post {
		success {
			cleanWs()
		}
	}
}