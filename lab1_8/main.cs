#include <stdio.h>
#define VAR 8

// 🧩 GLOBAL DATA DEFINITIONS
long int a, b, c, d;
long int a1, b1, c1, d1;
long left, right, left_a, right_a;
int err;

int main()
{
    // ⟳ MAIN COMPUTATIONAL LOOP
    for (;;)
    {
        // 1. INPUT PHASE
        printf("Input a b : ");
        scanf_s("%i %i", &a1, &b1);

        // 2. PRE-PROCESSING (C-Level division)
        a = a1 / (VAR + 2);
        b = b1 / (VAR + 3);

        // 3. C-LANGUAGE CALCULATION (Validation Baseline)
        right = ((a - b) * (a - b)) + 4 * a * b; // Right side identity
        left = (a + b) * (a + b);               // Left side identity

        int left_a, right_a;

        // ⚡ ASSEMBLER COMPUTATIONAL ENGINE
        __asm {
            // ➔ STEP 1: Input division (Preparing operands)
            mov eax, a1;
            mov ebx, VAR + 2;
            cdq
            idiv ebx
            mov esi, eax       // esi = processed a

            mov eax, b1;
            mov ebx, VAR + 3;
            cdq
            idiv ebx
            mov edi, eax       // edi = processed b
        }

        // ➔ STEP 2: Compute Left side ((a + b)^2)
        __asm {
            mov err, 0         // Reset error flag
            mov eax, esi       // eax = a
            mov ebx, edi       // ebx = b
            add eax, ebx       // eax = a + b
            mov edx, eax       
            imul edx, eax      // edx = (a + b)^2
            jo Error           // Check overflow
            mov left_a, edx    // Store result
        }

        // ➔ STEP 3: Compute Right side ((a - b)^2 + 4ab)
        __asm {
            mov eax, esi       // eax = a
            mov ebx, edi       // ebx = b
            mov esi, eax       // esi = a
            sub esi, ebx       // esi = a - b
            mov ecx, esi       // ecx = a - b
            imul ecx, esi      // ecx = (a - b)^2
            
            imul eax, ebx      // eax = a * b
            jo Error           // Check overflow
            
            imul eax, 4        // eax = 4 * a * b
            jo Error           // Check overflow
            
            add eax, ecx       // eax = (a - b)^2 + 4 * a * b
            mov right_a, eax   // Store result
            jmp Exit           // Success path
            
        Error:
            mov err, 1         // Overflow detected
        Exit:
        }

        // 4. OUTPUT PHASE
        if (err == 1)
        {
            printf("****Error: Overflow detected!****\n");
        }
        else
        {
            printf("(ASM)Result right: %i\n", right_a);
            printf("(ASM)Result left: %i\n", left_a);
        }

        // Display C-based results for verification
        printf("( C )Result right: %i\n", right);
        printf("( C )Result left: %i\n", left);
        printf("\n");
    }
    return 0;
}