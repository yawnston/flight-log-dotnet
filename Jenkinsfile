pipeline {
parameters {
  booleanParam defaultValue: false, description: 'Skips tests', name: 'SKIP_TESTS'
}
 agent any
    triggers {
  pollSCM 'H/3 * * * *'
}
 stages {
 stage('Checkout') {
     
 steps {
 echo 'Cleaning and checking out'
 checkout([$class: 'GitSCM', branches: [[name: 'pjaroschy/Test_Jenkins']], doGenerateSubmoduleConfigurations: false, extensions: [[$class: 'CleanBeforeCheckout', deleteUntrackedNestedRepositories: true]], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'f6cddd08-3d1f-45cb-a0f3-9af68ed1fd52', url: 'https://github.com/yawnston/flight-log-dotnet.git']]])
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
 step([$class: 'MSTestPublisher', testResultsFile: "**/*.trx", failOnError: true,
keepLongStdio: true])
 }
}
 }
}