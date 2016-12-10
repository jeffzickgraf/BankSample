
Guys - these aren't exhaustive tests, but I think they will give you the idea I know how to unit test
	Ideally in a larger application we would be using dependency injection so that it would be easy to
	isolate specific code under test by using a mocking framework since we could inject mocks in as our dependencies
	to control code flow with our fake implementations of interfaces and abstract classes. 

	I did use these to find bugs in my code as I wrote parts of the code. Didn't do full TDD but I do usually like to write
	unit tests alongside code when I get it partially implemented so I don't go too far along the process without unit tests
	  
	  
	Would probably also create base classes or helper methods or constants for provided values so we could be DRY but wanted to leave these
	simple for readability and get you back the sample as soon as possible.
	
	See the AccountRecorderTests.cs for a simple example of using a mocking framework to mock out objects and provide values.
		 
	The IntegrationTests were just for my own purposes as I built this application to ease development. If used by others would
	need to provide some appropriate paths for file locations in some cases for output
	