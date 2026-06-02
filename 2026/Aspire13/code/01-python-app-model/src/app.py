import time

print("Python worker started")
for i in range(5):
    print(f"tick {i}")
    time.sleep(1)
