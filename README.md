# CertTestRunnerScratch
Playing with doing xr sdk cert tests through test runner - both automated and manual

### Setup
- Import the target XR SDK package
- Configure ProjectSettings -> XR -> XR Manager to start the target XR SDK Loader
- Adjust project build settings to target the appropriate build platform.

### Running Tests
- Window -> General -> Test Runner -> Playmode Tests -> Automated Tests -> Run All in Player.
- OR run through the Test Runner command line interface for continuous integration. See bottom of https://docs.unity3d.com/Manual/PlaymodeTestFramework.html

### Running a Subset of Tests
- Window -> General -> Test Runner -> Playmode Tests. Select the tests you are interested, right click on them, and Run
- OR run through the Test Runner command line interface with the option <-testFilter "regexString">.  This allows you to filter by namespaces, class names, or test names.

### Running a Single Test Scene
Do not just hit play - this won't work!  Refer to "Running a subset of tests" above.

### Edit a Manual Test Scene
TODO

### Creating a New Manual Test Scene
TODO

### Creating a New Automated Test
- Navigate to Assets/Tests/foo, where foo is the specific subsystems or intra-subsystem integration that applies to your new test.  If no appropriate folder exists, refer to "Creating a New Subsystem Test Folder.
- Open \*.Automatic.cs and add your new test as a [Test] or [UnityTest] as described in the playmode test documentation https://docs.unity3d.com/Manual/PlaymodeTestFramework.html

### Creating a New Subsystem Test Folder
TODO
