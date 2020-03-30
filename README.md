# Recording Demonstrations Steps
1. Add Demonstration Recorder component in Agent component.
2. check the Record and write name and path
3. in the config.yaml add demo file path
```
behavioral_cloning:
    demo_path: <path_to_your_demo_file>
reward_signals:
    gail:
        demo_path: <path_to_your_demo_file>
```