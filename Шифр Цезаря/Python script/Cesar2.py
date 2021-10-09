from Cesar1 import *


def cesar2_for_word(word="word", key="key"):
    alphabet = create_alphabet()
    array_of_key_indexes = []
    array_of_chars = []
    for i in key:
        array_of_key_indexes.append(alphabet.index(i))

    print(array_of_key_indexes)
    array_of_z_indexes = []
    for i in range(len(word)):
        array_of_z_indexes.append(
            (alphabet.index(word[i]) +
             array_of_key_indexes[i % (len(array_of_key_indexes))] + 1) %
            (len(alphabet)))

    for i in array_of_z_indexes:
        array_of_chars.append(alphabet[i])

    return "".join(array_of_chars)


def cesar2(message="Hello world", key="key"):
    message = message.lower()
    key = key.lower()
    array_of_words = message.split()
    array_of_coded_words = []
    for word in array_of_words:
        array_of_coded_words.append(cesar2_for_word(word, key))

    return ' '.join(array_of_coded_words)
