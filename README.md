# vldaptest

The purpose of this command-line tool is to execute a set of shell commands and ensure that they produce expected output.

It takes a single argument - path to a JSON file that contains all required configuration data. 
Sample configuration file tests.json is provided, as well as sample test files under Expected folder.

vldaptest runs every command it finds in the configuration file, runs the command and compares the output against corresponding file in the Expected folder. At the end it prints either "SUCCESS!" or "FAILURE!" followed by a list of failed test ids.

The simplest way to create expected output files is to create empty files and run the tool once. You can then navigate to the timestamped subfolder under the "actual" folder and copy all files back into the "expected" folder.
