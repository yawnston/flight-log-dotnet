pipeline {
    agent any
    parameters {
        booleanParam defaultValue: false, description: '', name: 'SKIP_TESTS'
    }
    stages {
        stage('Checkout') {
            steps {
                echo 'Cleaning and checking out..'
                cleanWs()
                git 'https://github.com/yawnston/flight-log-dotnet.git'
            }
        }
        stage('Build') {
            steps {
                echo 'Building..'
                sh label: '', script: 'dotnet build'
            }
        }
        stage('Test') {
            when {
                not {
                    expression { params.SKIP_TESTS }
                }
            }
            steps {
                warnError('Tests failed') {
                    script {
                        sh 'dotnet test --filter DisplayName~FlightLogNet.Tests.Operation --logger "trx" '
                    }
                }
                step([$class: 'MSTestPublisher', testResultsFile: "**/*.trx", failOnError: true, keepLongStdio: true])
            }
        }
    }
}