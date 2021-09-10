def create_alphabet():
    alphabet_in_lower_case = []
    for char in range(97, 123):
        alphabet_in_lower_case.append(chr(char))

    return alphabet_in_lower_case
    pass


def rotate_alphabet(alphabet, offset):
    rotated_alphabet = alphabet.copy();

    # rotate alphabet using offset
    for i in range(offset):
        for j in range(len(rotated_alphabet) - 1, 0, -1):
            rotated_alphabet[j], rotated_alphabet[j - 1] = rotated_alphabet[j - 1], rotated_alphabet[j]

    return rotated_alphabet


def set_key_to_alphabet(alphabet, key):
    if (len(key) <= 0):
        return alphabet.copy()
    coded_alphabet = alphabet.copy()
    for i in range(len(key) - 1, -1, -1):
        coded_alphabet.pop(coded_alphabet.index(key[i]))
        coded_alphabet.insert(0, key[i])

    return coded_alphabet
    pass


def apply_cesar(alphabet, coded_alphabet, message):
    coded_message = []
    for i in range(len(message)):
        # array_of_indexes.append(alphabet.index(message[i]))
        coded_message.append(coded_alphabet[alphabet.index(message[i])])
    return coded_message


def cesar_for_word(word="testmessage", key="", offset=0):
    # key < alph
    # any key - uniq
    # Привет мир!
    str = ""
    alphabet = create_alphabet()
    print(alphabet)
    coded_alphabet = set_key_to_alphabet(alphabet, key)
    print(coded_alphabet)
    coded_alphabet = rotate_alphabet(coded_alphabet, offset)
    print(coded_alphabet)
    word = apply_cesar(alphabet, coded_alphabet, word)
    return str.join(word)


def cesar1(message="test message", key="", offset=0):
    message = message.lower()
    array_of_words = message.split()
    array_of_coded_words = []
    str = " "
    for word in array_of_words:
        array_of_coded_words.append(cesar_for_word(word, key, offset))

    return str.join(array_of_coded_words)
