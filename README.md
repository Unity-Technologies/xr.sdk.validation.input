# XR SDK Validation project
Automated and Manual tests to verify XR SDK providers.  Both automated and manual tests are executed through Test Runner to enable continuous integration systems.

Branches are named based on new input provider interface versions.  There is at most only one new interface version per dot release of unity (for example, 2019.2 and 2019.3 are different dot releases).  New versions of Unity may not contain a new interface version.  When choosing which branch to use, select the latest Unity version that does not exceed the version you wish to test with.

Does your platform need extra time to start up for one reason or another?  Navigate to Packages\com.unity.xr.certinputmanual\Runtime\ManualTests\_TestRunnerEntry\XRBaseTestFacilitator.cs and set InitializeTime to your magic number.

### Setup
- Import the target XR SDK package
- Configure ProjectSettings -> XR -> XR Manager to the target XR SDK Loader
- Adjust project build settings to target the appropriate build platform.

### Running Tests
- Window -> General -> Test Runner -> Playmode Tests -> Run All in Player.
- OR run through the Test Runner command line interface for continuous integration. See bottom of https://docs.unity3d.com/Manual/PlaymodeTestFramework.html

### Running a Subset of Tests
- If your Input Provider runs on PC, you can navigate to Window -> General -> Test Runner -> Playmode Tests. Select a test or set of tests, right click on them, and Run.
- OR run through the Test Runner command line interface with the option <-testFilter "regexString">.  This allows you to filter by namespaces, class names, or test names.

### Running a Single Test Scene
- If your Input Provider runs on PC, you can open the scene you want to run and use editor playmode.
- Otherwise, refer to "Running a Subset of Tests above.

### Edit a Manual Test Scene
- Manual test scenes are located in Packages\com.unity.xr.certinputmanual\Runtime\ManualTests.  Each test scene has a corresponding TestFacilitator script located in Packages\com.unity.xr.certinputmanual\Runtime\ManualTests\_TestRunnerEntry which controls the flow of the manual test in that scene.
- To run the test, see the "Running a Single Test Scene" section of this document.

### Creating a New Manual Test Scene
- Navigate to Packages\com.unity.xr.certinputmanual\Runtime\ManualTests
- Copy an existing scene and rename it.
- Create a new TestFacilitator in Packages\com.unity.xr.certinputmanual\Runtime\ManualTests\_TestRunnerEntry.  This can be a copy of an existing TestFacilitator renamed to reflect your new test name.  The TestFacilitator script controls the flow of your test and ultimately reports status back to the Test Runner.
- In your new scene replace the existing TestFacilitator script, located on the "TestFacilitator" GameObject, with your new TestFacilitator script.  Link the InstructionCanvas to this new Component.
- Edit the new scene and TestFacilitator script to match your desired functionality.  Packages\com.unity.test-runner-manual-tests\Samples\Example\Tests\TestScripts contains examples of manual checkpoints and correct status reporting.
- Add your new scene to the build settings scene list
- Add your Test Runner test to Packages\com.unity.xr.certinputmanual\Runtime\InputManual.cs.

### Creating a New Automated Test
- Automated input tests are in a separate package located at https://github.com/Unity-Technologies/com.unity.xr.certinputauto/
- This repository is pulled into xr.sdk.validation through the Packages/manifest.json file.  In order to pull in a new test version, you may need to delete the "lock" set of attributes in this file.

