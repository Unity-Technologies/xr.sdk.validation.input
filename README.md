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
- Window -> General -> Test Runner -> Playmode Tests. Select a test or set of tests, right click on them, and Run
- OR run through the Test Runner command line interface with the option <-testFilter "regexString">.  This allows you to filter by namespaces, class names, or test names.

### Running a Single Test Scene
Do not just hit play - this won't work!  Refer to "Running a subset of tests" above.

### Edit a Manual Test Scene
- Manual test scenes are located in Assets/ManualTests/ under subsystem folders.  Each test scene has a corresponding TestFacilitator script located in Assets/ManualTests/\_TestRunnerEntry which controls the flow of the manual test in that scene.
- To run the test, see the "Running a Single Test Scene" section of this document.

### Creating a New Manual Test Scene
- Navigate to Assets/ManualTests/foo, where foo is the specific subsystem that applies you will test.  If no appropriate folder exists refer to "Creating a New Subsystem Test Folder."
- Copy an existing scene or copy and rename Assets/ManualTests/TemplateSubsystem/TemplateScene to this folder.
- Create a new TestFacilitator in Assets/ManualTests/\_TestRunnerEntry.  This can be a copy of an existing TestFacilitator or a copy of Assets/ManualTests/\_TestRunnerEntry/TemplateTestFacilitator.cs.  The TestFacilitator script controls the flow of your test and ultimately reports status back to the Test Runner.
- In your new scene replace the existing TestFacilitator script, located on the "TestFacilitator" GameObject, with your new TestFacilitator script.
- Edit the new scene and TestFacilitator script to match your desired functionality.  Assets/ManualTests/\_TestRunnerEntry/TemplateTestFacilitator.cs contains examples of manual checkpoints and correct status reporting.

### Creating a New Automated Test
- Navigate to Assets/Tests/foo, where foo is the specific subsystem or intra-subsystem integration that applies to your new test.  If no appropriate folder exists refer to "Creating a New Subsystem Test Folder."
- Open \*Automatic.cs and add your new test as a [Test] or [UnityTest] as described in the playmode test documentation https://docs.unity3d.com/Manual/PlaymodeTestFramework.html

### Creating a New Subsystem Test Folder
- Check Assets/Tests/ for a folder that already matches the subsystem you want to write tests for.  If one exists, create a new test in that folder instead of creating a new subsystem folder.
- Copy Assets/Tests/TemplateSubsystem to Assets/Tests/foo, where foo is the name of the subsystem that you will be testing.
- If you want to create manual tests for this subsystem, copy Assets/ManualTests/TemplateSubsystem folder to Assets/ManualTests/foo, where foo is the name of the subsystem that you will be testing.
