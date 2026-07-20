#include "pch.h" 
#include <stdio.h> 
#include <iostream> 
#include <iomanip> 

using namespace std;

// 🧩 DATA DEFINITIONS: Boolean variables for logical functions
// Using 'unsigned char' is recommended for assembly compatibility to ensure 1-byte size
bool x3, x2, x1, x0, f1, f2, f3, f1_a, f2_a, f3_a;

int main()
{
    setlocale(LC_ALL, "Russian");
    
    // 📋 DISPLAY TASK INFORMATION
    printf("\n\tCalculate logical function values:");
    printf("\n\tF1(x3,x2,x1,x0)=(2,4,6,8,11,12,14)");
    printf("\n\tF2(x3,x2,x1,x0)=(1,3,9,10,13,14)");
    printf("\n\tF3(x3,x2,x1,x0)=(3,5,8,9,11)");

    // ⟳ MAIN COMPUTATIONAL LOOP
    for (;;)
    {
        printf("\nPlease, input boolean variables x3, x2, x1, x0: ");
        cin >> x3 >> x2 >> x1 >> x0;

        // ==========================================================
        // C++ LOGICAL CALCULATION (Reference implementation)
        // ==========================================================
        f1 = (x3 && !x2) || (x3 && !x1) || (!x3 && x2 && x1 && x0);
        f2 = (!x3 && x2) || (x3 && !x2 && !x1);
        f3 = (!x3 && x2 && x1) || (x3 && !x2 && !x1) || (x3 && !x2 && !x0);

        // ==========================================================
        // ASSEMBLER LOGICAL ENGINE (x86 assembly)
        // ==========================================================
        
        // ➔ F1 Calculation: (x3 & !x2) | (x3 & !x1) | (!x3 & x2 & x1 & x0)
        __asm {
            mov al, x3; mov ah, x2; not ah; and al, ah                          // (x3 & !x2)
            mov ah, x1; not ah; and ah, x3; or al, ah                           // | (x3 & !x1)
            mov ah, x3; not ah; and ah, x2; and ah, x1; and ah, x0; or al, ah   // | (!x3 & x2 & x1 & x0)
            mov f1_a, al                                                        // Store result
        };

        // ➔ F2 Calculation: (!x3 & x2) | (x3 & !x2 & !x1)
        __asm {
            mov al, x3; not al; and al, x2                                      // (!x3 & x2)
            mov ah, x2; not ah; mov bl, x1; not bl; and ah, bl; and ah, x3      // (x3 & !x2 & !x1)
            or al, ah                                                           // Combine terms
            mov f2_a, al                                                        // Store result
        };

        // ➔ F3 Calculation: (!x3 & x2 & x1) | (x3 & !x2 & !x1) | (x3 & !x2 & !x0)
        __asm {
            mov al, x3; not al; and al, x2; and al, x1                          // (!x3 & x2 & x1)
            mov ah, x2; mov bl, x1; not ah; not bl; and ah, bl; and ah, x3      // | (x3 & !x2 & !x1)
            or al, ah                                         
            mov ah, x2; mov bl, x0; not ah; not bl; and ah, bl; and ah, x3      // | (x3 & !x2 & !x0)
            or al, ah                                                           // Final OR operation
            mov f3_a, al                                                        // Store result
        };

        // 📤 OUTPUT PHASE
        int i, j, k, l, m;
        // Convert boolean to integer for bitwise operations
        i = (int)x3; j = (int)x2; k = (int)x1; l = (int)x0;
        
        // Calculate decimal representation (m) for the current input set
        m = (i << 3) + (j << 2) + (k << 1) + l;

        // Print results table
        cout << "                  ___C++__     ___Asm__" << endl
             << "Set" << "  " << setw(2) << "x3" << "x2" << "x1" << "x0"
             << "    " << "F1 " << "F2 " << "F3 "
             << "    " << "F1 " << "F2 " << "F3 " << endl;

        cout << "  " << setw(2) << m << "   " << i << " " << j << " " << k << " " << l
             << "     " << f1 << "  " << f2 << "  " << f3
             << "     " << f1_a << "  " << f2_a << "  " << f3_a << endl;
    }
    return 0;
}