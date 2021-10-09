#include <built_in.h>

#define TEMPO 146
#define WHOLE_NOTE_DUR 240000 / TEMPO
#define HALF_NOTE_DUR WHOLE_NOTE_DUR / 2
#define QUARTER_NOTE_DUR WHOLE_NOTE_DUR / 4
#define EIGTH_NOTE_DUR WHOLE_NOTE_DUR / 8
#define SIXTEENTH_NOTE_DUR WHOLE_NOTE_DUR / 16

int num_mask[] = {0x3F, 0x06, 0x5B, 0x4F, 0x66, 0x6D, 0x7D, 0x07, 0x7F, 0x6F}; // Nums with mask

unsigned short shifter, portd_index;
unsigned int digit, number, i = 0, loop_index = 0;
unsigned short portd_array[4] = {0, 0, 0, 0};

const int length = 9;
int j = 0;
int sequence[] = {1, 5, 20, 80, 192, 160, 40, 10, 3};

void Timer0Overflow_ISR() org IVT_ADDR_TIMER0_OVF
{
    PORTA = 0;                        // Turn off all 7seg displays
    PORTC = portd_array[portd_index]; // bring appropriate value to PORTC
    PORTA = shifter;                  // turn on appropriate 7seg. display

    // move shifter to next digit
    shifter <<= 1;
    if (shifter > 8u)
        shifter = 1;

    // increment portd_index
    portd_index++;
    if (portd_index > 3u)
        portd_index = 0; // turn on 1st, turn off 2nd 7seg.
}

void enter_value_to_portd_array(int value)
{
    while (value > 0)
    {
        digit = num_mask[value % 10];
        value /= 10;
        portd_array[i] = digit;
        i++;
    }
}

void task1()
{
    DDRD = 0xFF;
    while (1)
    {
        while (j < 9)
        {
            PORTD = sequence[j];
            enter_value_to_portd_array(PORTD);
            Sound_Play(1397, 100);
            Delay_ms(1000); // Do we need to change that?
            j++;
        }
        Sound_Play(440, 100);
        j--;
        while (j > 0)
        {
            PORTD = sequence[j];
            enter_value_to_portd_array(PORTD);
            Sound_Play(1318, 100);
            Delay_ms(1000); // Do we need to change that?
            j--;
        }
    }
}

void d5(int duration)
{
    Sound_Play(1244, duration);
}

void e5(int duration)
{
    Sound_Play(1318, duration);
}
void f5(int duration)
{
    Sound_Play(1396, duration);
}
void g5(int duration)
{
    Sound_Play(1568, duration);
}
void a5(int duration)
{
    Sound_Play(1720, duration);
}
void ad5(int duration)
{
    Sound_Play(1865, duration);
}

void b5(int duration)
{
    Sound_Play(1975, duration);
}

void play_popular_song()
{
    d5(QUARTER_NOTE_DUR);
    g5(QUARTER_NOTE_DUR);
    b5(EIGTH_NOTE_DUR);
    a5(HALF_NOTE_DUR);
    Delay_ms(EIGTH_NOTE_DUR);
    f5(EIGTH_NOTE_DUR);
    f5(EIGTH_NOTE_DUR);
    f5(SIXTEENTH_NOTE_DUR);
    e5(SIXTEENTH_NOTE_DUR);
    d5(EIGTH_NOTE_DUR);
    f5(EIGTH_NOTE_DUR);
    f5(EIGTH_NOTE_DUR);
    f5(SIXTEENTH_NOTE_DUR);
    e5(SIXTEENTH_NOTE_DUR);
    d5(SIXTEENTH_NOTE_DUR);
    Delay_ms(SIXTEENTH_NOTE_DUR);
    g5(SIXTEENTH_NOTE_DUR);
    a5(SIXTEENTH_NOTE_DUR);
    ad5(SIXTEENTH_NOTE_DUR);
    Delay_ms(SIXTEENTH_NOTE_DUR);
    g5(SIXTEENTH_NOTE_DUR);
    a5(SIXTEENTH_NOTE_DUR);
    ad5(SIXTEENTH_NOTE_DUR);
    Delay_ms(SIXTEENTH_NOTE_DUR);
    g5(SIXTEENTH_NOTE_DUR);
    a5(SIXTEENTH_NOTE_DUR);
    ad5(SIXTEENTH_NOTE_DUR);
    Delay_ms(HALF_NOTE_DUR);
    d5(QUARTER_NOTE_DUR);
    g5(QUARTER_NOTE_DUR);
    ad5(EIGTH_NOTE_DUR);
    a5(HALF_NOTE_DUR);
    Delay_ms(EIGTH_NOTE_DUR);
    d5(QUARTER_NOTE_DUR);
    g5(QUARTER_NOTE_DUR);
    ad5(EIGTH_NOTE_DUR);
    a5(HALF_NOTE_DUR);
    Delay_ms(EIGTH_NOTE_DUR);
    f5(EIGTH_NOTE_DUR);
    f5(EIGTH_NOTE_DUR);
    f5(SIXTEENTH_NOTE_DUR);
    e5(SIXTEENTH_NOTE_DUR);
    d5(EIGTH_NOTE_DUR);
    f5(EIGTH_NOTE_DUR);
    f5(EIGTH_NOTE_DUR);
    f5(SIXTEENTH_NOTE_DUR);
    e5(SIXTEENTH_NOTE_DUR);
    d5(SIXTEENTH_NOTE_DUR);
    Delay_ms(SIXTEENTH_NOTE_DUR);
    ad5(EIGTH_NOTE_DUR);
    g5(EIGTH_NOTE_DUR);
}

void main()
{
    // Initialize ports, timers, flags etc.
    {
        DDRA = 0x0f;
        PORTA = 0;
        DDRC = 0xff;
        PORTC = 0;

        digit = 0;
        portd_index = 0;
        shifter = 1; // Initial number value

        TCCR0 = 0x03; // ClkI/O/64 (From prescaler)

        SREG_I_bit = 1; // Interrupt enable
        TOIE0_bit = 1;  // Timer0 overflow interrupt enable

        Sound_Init(&PORTB, 1); // Initialize sound pin
    }

    task1();

    play_popular_song();
}
