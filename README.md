# ⚡ x86 Assembly & Low-Level Computer Architecture Labs

A comprehensive collection of computer architecture lab assignments. Includes standalone **16-bit 8086 Assembly** scripts for `emu8086` and **32-bit x86 C++ Inline Assembly** projects for `Visual Studio`. A collection of old university lab assignments I dug up and neatly organized for GitHub

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


## 📋 Part 1: Standalone 8086 Assembly (`emu8086`)

### 🔹 Lab 1: Array Reversing (`code.asm`)
Copies elements from one array (`tabl1`) to another (`tabl2`) in reverse order.
* **Source array:** `1, 2, 3, 4`
* **Result array:** `4, 3, 2, 1`

<p align="center">
  <img src="access/1.png" alt="Reversed Array Variables" width="350"/>
</p>


# 🔹 Lab 2: Character Swapping (`code2.asm`)
Reads two characters typed by the user, swaps them, and outputs them in reverse order.
* **Input:** User types two symbols (e.g., `1` and `2`).
* **Output:** Program outputs them swapped (as `2` and `1`).

<p align="center">
  <img src="access/2.png" alt="Character Swapping Console" width="350"/>
</p>

---

## 🛠️ Part 2: C++ & Inline x86 Assembly (`Visual Studio`)

### 🔹 Lab 1_8: Linear Arithmetic & Overflow Checking (`lab1_8`)

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


# ⚡ Lab 2_8: x86 Bitwise Logic & Boolean Functions

<p align="center">
  🎓 <i>Dug up my old university computer architecture lab focusing on low-level boolean logic and Karnaugh map minimization.</i>
</p>

### 📌 Overview

This lab demonstrates how to evaluate 4-variable boolean functions $F(x_3, x_2, x_1, x_0)$ using minimized boolean expressions and bitwise x86 Assembly instructions (`AND`, `OR`, `NOT`). The results are verified against standard C++ logical operations across all 16 truth table input combinations.

## 🗺️ Karnaugh Maps (Minimization)

Below are the Karnaugh maps used to minimize the boolean functions $F_1$, $F_2$, and $F_3$:

<p align="center">
  <img src="lab2_8/access/1.png" alt="Karnaugh Map F1" width="380"/>
  <br>
  <sub><b>Figure 1:</b> Karnaugh Map for $F_1 = (7, 8, 9, 10, 11, 12, 13)$</sub>
</p>

<br>

<p align="center">
  <img src="lab2_8/access/2.png" alt="Karnaugh Maps F2 and F3" width="380"/>
  <br>
  <sub><b>Figure 2:</b> Karnaugh Maps for $F_2 = (4, 5, 6, 7, 8, 9)$ and $F_3 = (6, 7, 8, 9, 10)$</sub>
</p>

### 📐 Minimized Boolean Functions

After Karnaugh Map minimization, the three target functions are expressed as:

* **$F_1$** $= (x_3 \land \neg x_2) \lor (x_3 \land \neg x_1) \lor (\neg x_3 \land x_2 \land x_1 \land x_0)$
* **$F_2$** $= (\neg x_3 \land x_2) \lor (x_3 \land \neg x_2 \land \neg x_1)$
* **$F_3$** $= (\neg x_3 \land x_2 \land x_1) \lor (x_3 \land \neg x_2 \land \neg x_1) \lor (x_3 \land \neg x_2 \land \neg x_0)$

### ⚙️ Assembly Implementation Details

Instead of standard high-level conditional branches (`if`/`else`), the low-level version uses 8-bit registers (`AL`, `AH`, `BL`) and bitwise instructions:

* `not reg` — Inverts boolean state.
* `and reg1, reg2` — Computes logical conjunction ($\land$).
* `or reg1, reg2` — Computes logical disjunction ($\lor$).

### 📊 Complete Truth Table (16 Input Sets)

| Set | $x_3$ | $x_2$ | $x_1$ | $x_0$ | C++ ($F_1, F_2, F_3$) | Assembly ($F_1, F_2, F_3$) | Match? |
|:---:|:---:|:---:|:---:|:---:|:---------------------:|:--------------------------:|:------:|
| **0**  | `0` | `0` | `0` | `0` | `0, 0, 0` | `0, 0, 0` | ✅ |
| **1**  | `0` | `0` | `0` | `1` | `0, 0, 0` | `0, 0, 0` | ✅ |
| **2**  | `0` | `0` | `1` | `0` | `0, 0, 0` | `0, 0, 0` | ✅ |
| **3**  | `0` | `0` | `1` | `1` | `0, 1, 0` | `0, 1, 0` | ✅ |
| **4**  | `0` | `1` | `0` | `0` | `0, 1, 0` | `0, 1, 0` | ✅ |
| **5**  | `0` | `1` | `0` | `1` | `0, 1, 0` | `0, 1, 0` | ✅ |
| **6**  | `0` | `1` | `1` | `0` | `0, 1, 1` | `0, 1, 1` | ✅ |
| **7**  | `0` | `1` | `1` | `1` | `1, 1, 1` | `1, 1, 1` | ✅ |
| **8**  | `1` | `0` | `0` | `0` | `1, 1, 1` | `1, 1, 1` | ✅ |
| **9**  | `1` | `0` | `0` | `1` | `1, 1, 1` | `1, 1, 1` | ✅ |
| **10** | `1` | `0` | `1` | `0` | `1, 0, 1` | `1, 0, 1` | ✅ |
| **11** | `1` | `0` | `1` | `1` | `1, 0, 0` | `1, 0, 0` | ✅ |
| **12** | `1` | `1` | `0` | `0` | `1, 0, 0` | `1, 0, 0` | ✅ |
| **13** | `1` | `1` | `0` | `1` | `1, 0, 0` | `1, 0, 0` | ✅ |
| **14** | `1` | `1` | `1` | `0` | `0, 0, 0` | `0, 0, 0` | ✅ |
| **15** | `1` | `1` | `1` | `1` | `0, 0, 0` | `0, 0, 0` | ✅ |




## 🚀 How to Run

### 1️⃣ For Standalone Assembly (`code.asm`, `code2.asm`)
1. Open `code.asm` or `code2.asm` in **emu8086**.
2. Click **Emulate** -> **Run**.

### 2️⃣ For C++ Inline Assembly (`lab1_8`, `lab2_8`)
1. Open the project folder in **Visual Studio**.
2. Set build platform to **x86 / Win32** (Required for `__asm` blocks).
3. Build and Run `main.cpp`.
