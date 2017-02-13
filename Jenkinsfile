node {
    stage('Clone'){
		checkout scm
	}

	stage('Clean'){
		sh('git clean -xdff')
	}

    stage('Build'){
		msbuild()
	}

	stage('Test'){
		mono('UnitTests/bin/Release/UnitTests.exe','')
		step([$class: "NUnitPublisher", testResultsPattern: "TestResult.xml"])
	}

	stage('Publish'){
		publish('Masterplan/bin/Release/Masterplan.exe')
	}
    
    stage('Archive'){
		archive '**/bin/Release/'
	}

	stage('Post-Build'){
		step([$class: 'WarningsPublisher', canComputeNew: false, canResolveRelativePaths: false, consoleParsers: [[parserName: 'MSBuild']], defaultEncoding: '', excludePattern: '', healthy: '', includePattern: '', messagesPattern: '', unHealthy: ''])
	}

}
