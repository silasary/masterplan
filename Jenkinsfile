node {
    stage('Clone'){
		checkout scm
	}

	stage('Clean'){
	    if (isUnix())
		{
			sh('git clean -xdff')
		}
		else
		{
			bat('git clean -xdff')
		}
	}

    stage('Build'){
		//branch = sh(script: 'git rev-parse --abbrev-ref HEAD', returnStdout: true)
		branch = env.BRANCH_NAME
		echo("BRANCH=${branch}")
		
		if (branch == "stable"){
			msbuild()
		}
		msbuild.debug()
	}

	stage('Test'){
		mono('UnitTests/bin/Debug/UnitTests.exe','')
		//junit "TestResult.xml"
	}

	stage('Publish'){
		if (branch == "stable"){
			publish('--generate Masterplan/bin/Release/Masterplan.exe --deploymentUrl http://masterplan.vorpald20.com/Masterplan.application --generateBootstrap setup.exe')
		}
		else {
			publish('--generate Masterplan/bin/Debug/Masterplan.exe  --deploymentUrl http://beta.masterplan.vorpald20.com/Masterplan.application --generateBootstrap setup.exe')
		}
	}
    
    stage('Archive'){
		archive '**/bin/**/'
		archive 'www/'
	}

	stage('Post-Build'){
		step([$class: 'WarningsPublisher', canComputeNew: false, canResolveRelativePaths: false, consoleParsers: [[parserName: 'MSBuild']], defaultEncoding: '', excludePattern: '', healthy: '', includePattern: '', messagesPattern: '', unHealthy: ''])
	}
}
