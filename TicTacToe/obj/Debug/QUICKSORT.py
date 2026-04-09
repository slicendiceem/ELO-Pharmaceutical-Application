import tkinter as tk
from tkinter import ttk, messagebox
import time

class QuickSortVisualizer:
    def __init__(self, root):
        self.root = root
        self.root.title("QuickSort Visualizer")
        
        # Configure main window
        self.root.geometry("800x600")
        self.root.resizable(True, True)
        
        # Create main frames
        self.input_frame = ttk.Frame(root, padding="10")
        self.visualization_frame = ttk.Frame(root, padding="10")
        self.control_frame = ttk.Frame(root, padding="10")
        
        self.input_frame.pack(fill=tk.BOTH, expand=True)
        self.visualization_frame.pack(fill=tk.BOTH, expand=True)
        self.control_frame.pack(fill=tk.BOTH, expand=True)
        
        # Input elements
        ttk.Label(self.input_frame, text="Enter numbers (comma or space separated):").pack(anchor=tk.W)
        self.input_entry = ttk.Entry(self.input_frame, width=50)
        self.input_entry.pack(fill=tk.X, padx=5, pady=5)
        
        self.input_button = ttk.Button(self.input_frame, text="Sort", command=self.start_sorting)
        self.input_button.pack(pady=5)
        
        # Visualization area
        self.canvas = tk.Canvas(self.visualization_frame, bg="white", height=400)
        self.canvas.pack(fill=tk.BOTH, expand=True)
        
        # Control buttons
        self.step_button = ttk.Button(self.control_frame, text="Step", command=self.next_step, state=tk.DISABLED)
        self.step_button.pack(side=tk.LEFT, padx=5)
        
        self.play_button = ttk.Button(self.control_frame, text="Play", command=self.play_sorting, state=tk.DISABLED)
        self.play_button.pack(side=tk.LEFT, padx=5)
        
        self.reset_button = ttk.Button(self.control_frame, text="Reset", command=self.reset)
        self.reset_button.pack(side=tk.RIGHT, padx=5)
        
        # Sorting variables
        self.array = []
        self.steps = []
        self.current_step = 0
        self.is_playing = False
        self.delay = 1000  # ms between steps when playing
        
        # Explanation text
        self.explanation_var = tk.StringVar()
        self.explanation_label = ttk.Label(self.control_frame, textvariable=self.explanation_var, wraplength=700)
        self.explanation_label.pack(fill=tk.X, pady=10)
        
    def parse_input(self):
        input_str = self.input_entry.get()
        try:
            # Replace commas with spaces and split
            numbers = input_str.replace(',', ' ').split()
            self.array = [int(num) for num in numbers]
            return True
        except ValueError:
            messagebox.showerror("Error", "Please enter valid integers separated by commas or spaces")
            return False
    
    def start_sorting(self):
        if not self.parse_input():
            return
            
        # Generate all steps of the quicksort
        self.steps = []
        self.quick_sort_with_steps(self.array.copy(), 0, len(self.array)-1, [])
        
        self.current_step = 0
        self.display_step()
        
        self.step_button["state"] = tk.NORMAL
        self.play_button["state"] = tk.NORMAL
    
    def quick_sort_with_steps(self, arr, low, high, context):
        if low < high:
            # Partition the array and get the pivot index
            pi, partition_steps = self.partition_with_steps(arr, low, high)
            
            # Add all partition steps to our steps list
            for step in partition_steps:
                self.steps.append((arr.copy(), low, high, pi, step[0], step[1]))
            
            # Recursively sort elements before and after partition
            self.quick_sort_with_steps(arr, low, pi-1, context + ["left"])
            self.quick_sort_with_steps(arr, pi+1, high, context + ["right"])
    
    def partition_with_steps(self, arr, low, high):
        steps = []
        pivot = arr[high]
        i = low - 1
        
        steps.append(("Start", f"Choosing pivot: {pivot} (last element)"))
        
        for j in range(low, high):
            steps.append(("Compare", f"Comparing {arr[j]} with pivot {pivot}"))
            if arr[j] <= pivot:
                i += 1
                if i != j:
                    arr[i], arr[j] = arr[j], arr[i]
                    steps.append(("Swap", f"Swapping {arr[i]} and {arr[j]}"))
        
        if i+1 != high:
            arr[i+1], arr[high] = arr[high], arr[i+1]
            steps.append(("Final swap", f"Moving pivot to correct position: swapping {arr[i+1]} and {arr[high]}"))
        
        return i+1, steps
    
    def display_step(self):
        if self.current_step >= len(self.steps):
            self.explanation_var.set("Sorting complete!")
            self.step_button["state"] = tk.DISABLED
            self.play_button["state"] = tk.DISABLED
            return
            
        arr, low, high, pi, action, explanation = self.steps[self.current_step]
        self.explanation_var.set(f"Step {self.current_step+1}/{len(self.steps)}: {action} - {explanation}")
        
        self.draw_array(arr, low, high, pi)
    
    def draw_array(self, arr, low=None, high=None, pi=None):
        self.canvas.delete("all")
        
        if not arr:
            return
            
        canvas_width = self.canvas.winfo_width()
        canvas_height = self.canvas.winfo_height()
        
        bar_width = canvas_width / len(arr)
        max_val = max(arr) if arr else 1
        
        for i, value in enumerate(arr):
            # Calculate coordinates
            x0 = i * bar_width
            y0 = canvas_height
            x1 = x0 + bar_width
            y1 = canvas_height - (value / max_val) * (canvas_height - 20)
            
            # Determine color based on current step
            fill_color = "sky blue"
            if low is not None and high is not None:
                if i == pi:
                    fill_color = "red"  # Pivot
                elif low <= i <= high:
                    fill_color = "light green"  # Current partition
                elif i < low or i > high:
                    fill_color = "light gray"  # Already sorted
            
            # Draw the bar
            self.canvas.create_rectangle(x0, y0, x1, y1, fill=fill_color, outline="black")
            
            # Draw the value
            self.canvas.create_text(x0 + bar_width/2, y1 - 10, text=str(value))
    
    def next_step(self):
        if self.current_step < len(self.steps):
            self.current_step += 1
            self.display_step()
    
    def play_sorting(self):
        if self.is_playing:
            self.is_playing = False
            self.play_button["text"] = "Play"
            return
            
        self.is_playing = True
        self.play_button["text"] = "Pause"
        self.play_next_step()
    
    def play_next_step(self):
        if not self.is_playing or self.current_step >= len(self.steps):
            self.is_playing = False
            self.play_button["text"] = "Play"
            return
            
        self.next_step()
        self.root.after(self.delay, self.play_next_step)
    
    def reset(self):
        self.array = []
        self.steps = []
        self.current_step = 0
        self.is_playing = False
        self.play_button["text"] = "Play"
        self.explanation_var.set("")
        self.canvas.delete("all")
        self.input_entry.delete(0, tk.END)
        self.step_button["state"] = tk.DISABLED
        self.play_button["state"] = tk.DISABLED

if __name__ == "__main__":
    root = tk.Tk()
    app = QuickSortVisualizer(root)
    root.mainloop()