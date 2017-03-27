from chatterbot import ChatBot

chatbot = ChatBot(
    'hitchBOT',
    trainer='chatterbot.trainers.ChatterBotCorpusTrainer'
)

# Train based on the english corpus
chatbot.train("chatterbot.corpus.english")

# Get a response to an input statement
while True:
    str_input = raw_input(">>")
    print chatbot.get_response(str_input)
