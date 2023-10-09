This is a simple experiment management program that allows you to add, retrieve, and manage experiments associated with devices.

1. If a device has received a value once, it will always receive only that value.
In the screenshot below, for a previously created device with an experiment, only this button (referring to the red button) is returned.
<img width="1419" alt="image" src="https://github.com/Denshchyk/ABtesting/assets/102922140/c630c072-5150-4a7a-b919-900ebb0447ce">

2. An experiment is conducted only for new devices: if the experiment is created after the first request from the device, the device should not know anything about this experiment.
In this case, the device knows nothing about the experiment.
<img width="1437" alt="image" src="https://github.com/Denshchyk/ABtesting/assets/102922140/76db5b0e-6bd0-4343-aca2-faf89f30752f">

3.Statistics in JSON format with a list of experiments, the total number of devices participating in the experiment, and their distribution among options.
So, we can get a list of experiments.
<img width="1409" alt="image" src="https://github.com/Denshchyk/ABtesting/assets/102922140/c9d2e679-5194-4fa1-bedd-8cbe55e9b72d">
Get total number of devices participating in the experiment.
<img width="1398" alt="image" src="https://github.com/Denshchyk/ABtesting/assets/102922140/d2e1de9e-dd6b-429f-8aa3-ad3b2a4445a1">
And we can get their distribution among options.
<img width="1404" alt="image" src="https://github.com/Denshchyk/ABtesting/assets/102922140/9b2ff1ec-ecef-4714-89b0-af0d91455a6f">
