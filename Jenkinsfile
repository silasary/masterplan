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
		//junit "TestResult.xml"
	}

	stage('Publish'){
		publish('Masterplan/bin/Release/Masterplan.exe')
	}
    
    stage('Archive'){
		archive '**/bin/Release/'
		archive 'www/'
	}

	stage('Post-Build'){
		step([$class: 'WarningsPublisher', canComputeNew: false, canResolveRelativePaths: false, consoleParsers: [[parserName: 'MSBuild']], defaultEncoding: '', excludePattern: '', healthy: '', includePattern: '', messagesPattern: '', unHealthy: ''])
	}

}
