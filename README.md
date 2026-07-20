# ⚡ x86 Assembly & Low-Level Computer Architecture Labs

A comprehensive collection of computer architecture lab assignments. Includes standalone **16-bit 8086 Assembly** scripts for `emu8086` and **32-bit x86 C++ Inline Assembly** projects for `Visual Studio`.

📜 A collection of old university lab assignments I dug up and neatly organized for GitHub.
---

## 📂 Repository Structure

```text
.
├── 📂 lab1_8/            # C++ Inline x86 Lab 1: Linear Arithmetic & Overflow
│   ├── 📂 access/        # Screenshots (1.png)
│   └── 📄 main.cpp       # C++ with __asm blocks
├── 📂 lab2_8/            # C++ Inline x86 Lab 2: Logic & Branching
│   ├── 📂 access/        # Screenshots (1.png, 2.png)
│   └── 📄 main.cpp       # C++ with __asm blocks
├── 📂 access/            # Screenshots for emu8086 scripts (1.png, 2.png)
├── 📄 code.asm           # Standalone 8086 Assembly: Array Reversing
├── 📄 code2.asm          # Standalone 8086 Assembly: Character Swapping
└── 📄 README.md          # Project documentation
```

---

## 📋 Part 1: Standalone 8086 Assembly (`emu8086`)

### 🔹 Lab 1: Array Reversing (`code.asm`)
Copies elements from one array (`tabl1`) to another (`tabl2`) in reverse order.
* **Source array:** `1, 2, 3, 4`
* **Result array:** `4, 3, 2, 1`

<p align="center">
  <img src="access/1.png" alt="Reversed Array Variables" width="350"/>
</p>

---

### 🔹 Lab 2: Character Swapping (`code2.asm`)
Reads two characters typed by the user, swaps them, and outputs them in reverse order.
* **Input:** User types two symbols (e.g., `1` and `2`).
* **Output:** Program outputs them swapped (as `2` and `1`).

<p align="center">
  <img src="access/2.png" alt="Character Swapping Console" width="350"/>
</p>

---

## 🛠️ Part 2: C++ & Inline x86 Assembly (`Visual Studio`)

### 🔹 Lab 1: Linear Arithmetic & Overflow Checking (`lab1_8`)

Calculates and compares identity formulas in both C++ and x86 Assembly:
<p align="center">
  $$\text{Left} = (a - b)^2 + 4ab$$
  $$\text{Right} = (a + b)^2$$
</p>


*Algebraically, both sides are identical: 
<p align="center">
  $(a - b)^2 + 4ab = (a + b)^2$.*
</p>

#### 📊 Test Cases & Results Explained

| # | Input $a_1$ | Input $b_1$ | Scaled $a$ | Scaled $b$ | C Output | Assembly Output | What Happens & Why |
|---|-------------|-------------|------------|------------|----------|-----------------|--------------------|
| **1** | `40` | `50` | `4` | `4` | `64` | `64` | **Positive:** $(4 + 4)^2 = 64$. Matches. |
| **2** | `-40` | `50` | `-4` | `4` | `0` | `0` | **Opposite signs:** $(-4 + 4)^2 = 0$. Matches. |
| **3** | `-40` | `-50` | `-4` | `-4` | `64` | `64` | **Negative:** $(-4 + -4)^2 = 64$. Matches. |
| **4** | `40` | `-50` | `4` | `-4` | `0` | `0` | **Opposite signs:** $(4 - 4)^2 = 0$. Matches. |
| **5** | `500000` | `400000` | `50000` | `36363` | `-1287548476` | `****Eror****` | **Overflow!** See explanation below. |

#### 💡 Why Assembly handles Overflow better than C
* **In C:** When standard 32-bit integers exceed memory limits (>2.1B), C **silently overflows** and prints meaningless corrupted numbers (`-1287548476`).
* **In Assembly:** After multiplication (`imul`), assembly checks the **Overflow Flag** (`jo Error`). If overflow occurs, it instantly jumps to an error block and prints `****Eror****`.

<p align="center">
  <img src="lab1_8/access/1.png" alt="Lab 1 Execution Screenshot" width="350"/>
</p>

---

### 🔹 Lab 2: Low-Level Logic & Branching (`lab2_8`)
Focuses on conditional jumps (`jmp`, `je`, `jne`, `jg`, `jl`), flag monitoring, and register control flow in Assembly.

---

## 🚀 How to Run

### 1️⃣ For Standalone Assembly (`code.asm`, `code2.asm`)
1. Open `code.asm` or `code2.asm` in **emu8086**.
2. Click **Emulate** -> **Run**.

### 2️⃣ For C++ Inline Assembly (`lab1_8`, `lab2_8`)
1. Open the project folder in **Visual Studio**.
2. Set build platform to **x86 / Win32** (Required for `__asm` blocks).
3. Build and Run `main.cpp`.
