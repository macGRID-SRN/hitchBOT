#include <SPI.h> // library for serial interface Bus
#include <WiFi.h> // library for connecting to Internet through WIFI Shield
#include <Twitter.h> //library for Twitter via website
#include <TVout.h> //library that will output either NTSC or PAL composite.
#include <fontALL.h> //composite video output library for AVR/Arduino
#include <avr/pgmspace.h> //library for generating composite video on a single AVR chip
TVout tv;
unsigned char x,y;
unsigned char originx = 5;
unsigned char originy = 80;
unsigned char plotx = originx;
unsigned char ploty = 40;
char s[32];
unsigned int n = 0;
int index = 0;
int messageLen = 32;
// char string for each sentence
prog_char string_0[] PROGMEM = "But we are for wild beasts! Let us throw us in my way.";
prog_char string_1[] PROGMEM = "What can only cure forces of the railway station and admirations of the words!";
prog_char string_2[] PROGMEM = "Instead, lift up to now magnified pensive charred fingers";
prog_char string_3[] PROGMEM = "come again our burning with my face cover it with a noise on earth.";
prog_char string_4[] PROGMEM = "Its red hot poker offering smoke; bridges with explosive character.";
prog_char string_5[] PROGMEM = "Public dormitories of the flapping of aggressive immense";
prog_char string_6[] PROGMEM = "and tottering muck which obstruct the unknown, to fishermen and this marvel. ";
prog_char string_7[] PROGMEM = "With patient at the clouds, nor yet a cruelty,";
prog_char string_8[] PROGMEM = "injustice will, hastily, deliciously, with the Victory gutter!";
prog_char string_9[] PROGMEM = "A factory of crucified pensive immobility into the anarchists,";
prog_char string_10[] PROGMEM ="tourished, trams that we could on its gangrene of Byzanting men,";
prog_char string_11[] PROGMEM ="The essentine knife militarian cowardice.";
prog_char string_12[] PROGMEM ="We went least time in the ideas which cover heels,";
prog_char string_13[] PROGMEM ="like fruit spiced with beings you do not know just what prolonged supervision of dogs.";
prog_char string_14[] PROGMEM ="And strong and celestial grime, amidst the limits";
prog_char string_15[] PROGMEM = "of fishermen and we have already promontortions? All right.";
prog_char string_16[] PROGMEM = "It rose slowly leave good incendiary violent at the hunted,";
prog_char string_17[] PROGMEM = "diminism and sculptors which seems to museum ever with the sword which kill.";
prog_char string_18[] PROGMEM = "Take the centurous steering men than the least ten year,";
prog_char string_19[] PROGMEM = "as our poetry will be could allow! We can you sleeplessness, the fist.";
prog_char string_20[] PROGMEM = "We want to sing the great sweep of me at each other";
prog_char string_21[] PROGMEM = "in the gates of casting muck which the admire an outpost,";
prog_char string_22[] PROGMEM = "from its black fur dappled with and sculptors";
prog_char string_23[] PROGMEM = "which rage and academies this manuscripts!";
prog_char string_24[] PROGMEM = "The oldest among us are not yet a cruel Queen to demolish";
prog_char string_25[] PROGMEM = "our books of the future is no masterpiece that we want to accomplaint prayer";
prog_char string_26[] PROGMEM = "offering serpents with its of their eyes. ";
prog_char string_27[] PROGMEM = "For they are not know just what prolongations devouring its of creaking smoke-";
prog_char string_28[] PROGMEM = "bridges with great-breasts! Let us from they are only that had said them";
prog_char string_29[] PROGMEM = "to be rid of the sum and breast ten year at the black fur dappled";
prog_char string_30[] PROGMEM = "with the diabolic cutlery of comfort. We are not to see they are";
prog_char string_31[] PROGMEM = "nourist guides and tentative gesture is no masterpiece that we shape of";
prog_char string_32[] PROGMEM = "the stars encamped into the beauty exists what prolonged supervision of beard,";
prog_char string_33[] PROGMEM = "suddenly distracter. Public dormitories with my friends!";
prog_char string_34[] PROGMEM = "Let thirty, and Space died yesterday. What can only the unknown,";
prog_char string_35[] PROGMEM = "not to demolish our courage, audacity of Samothrace.";
prog_char string_36[] PROGMEM = "We are notes of staid fish up my car, like a corpse on its orbit.";
prog_char string_37[] PROGMEM = "There younger and drove of puppies biting with the paper basket";
prog_char string_38[] PROGMEM = "like a visit once again beneath with a noise our heart.";
prog_char string_39[] PROGMEM = "A crowd of the belly of creation of puddles. Let 30 year at the birth";
prog_char string_40[] PROGMEM = "of breath our challenge to the too long their stupid swaying spirits,";
prog_char string_41[] PROGMEM = "along mine the wheel a guillotine knife,";
prog_char string_42[] PROGMEM = "the perilous leap of gymnasts flung acrossable museums and";
prog_char string_43[] PROGMEM = "for art can only beneath the must open the good enough for their eyes.";
prog_char string_44[] PROGMEM = "For the full of madness brought us from its upholstery";
prog_char string_45[] PROGMEM = "of ruinous railway stations in the sea.";
prog_char string_46[] PROGMEM = "Then we want to glorify war a guillotine rings! No reasons. ";
prog_char string_47[] PROGMEM = "The essential grime, amidst the catacombs of the past,";
prog_char string_48[] PROGMEM = "facing at the admirable must breath, tamed, smell is good incendiaries!";
prog_char string_49[] PROGMEM = "Truly identical eyes. For artist trying got in my way.";
prog_char string_50[] PROGMEM = "We want to the you? it is in a useless manuscripts!";
prog_char string_51[] PROGMEM = "They will have already created our insolent challenge";
prog_char string_52[] PROGMEM = "to be rid of the best part of aggressive immense prolonged supervision by";
prog_char string_53[] PROGMEM = "the blows of waste the birth of their first angels fly!";
prog_char string_54[] PROGMEM = "We drove us through their celestial bivouacs. Alone with patient and color.";
prog_char string_55[] PROGMEM = "To make a hideous hurl the catacombs of the wretching equals to care them!";
prog_char string_56[] PROGMEM = "Of courage! We have none of professors, itself with all ourselves?";
prog_char string_57[] PROGMEM = "Do you sleep side by side for prisoners in my way. What a bore! ";
prog_char string_58[] PROGMEM = "Pouah! I stopped short, and living motor car which rage ";
prog_char string_59[] PROGMEM = "in our proud indefatigable and its fins. The poet must be the propeller souls,";
prog_char string_60[] PROGMEM = "because we were filled the extreme propeller sounds, the forty let young,";
prog_char string_61[] PROGMEM = "strong and exasperated by workshops beneath mosque lamps in the padlocks!";
prog_char string_62[] PROGMEM = "Let us go! Here is to museums, like serpents; factories of the Victory ";
prog_char string_63[] PROGMEM = "of the applause we hungry automobiles roared ";
prog_char string_64[] PROGMEM = "beneath our beautiful false intelligent young lions, death the impossible?";
prog_char string_65[] PROGMEM = "Time and test the more hatred and libraries where young men, drunkards ";
prog_char string_66[] PROGMEM = "beating its gangrene of danger, that has not from afar, like two cyclists,";
prog_char string_67[] PROGMEM = "the world's summit, we, the throw us in Italy has been too long ";
prog_char string_68[] PROGMEM = "their breasted treasure and our might, my friends!, I savored a mouthful ";
prog_char string_69[] PROGMEM = "of sunny rivers: adventurous shutters of life to the monotonous and slumber.";
prog_char string_70[] PROGMEM = "We are not yet we had no ideal Mistress of life to be present speed.";
prog_char string_71[] PROGMEM = "As we launch once a year, as one with violent electric heart.";
prog_char string_72[] PROGMEM = "A crowd around. It rose slowly leaving our native slowly";
prog_char string_73[] PROGMEM = "leave good sense primordial elements. Here the Po in front of muddy water!";
prog_char string_74[] PROGMEM = "I stopped short, calvaries. Italy has not an aggression, feverish sleep side";
prog_char string_75[] PROGMEM = "forty let us feed the lighthouses. Death, our task. When we must be them.";
prog_char string_76[] PROGMEM = "But we cannot an aggression of today will come! Heap up to now magnified";
prog_char string_77[] PROGMEM = "pensive charred fingers come! Heap up to now magnified around.";
prog_char string_78[] PROGMEM = "It is because you want to despair, but simply to increase there were ";
prog_char string_79[] PROGMEM = "two cyclists disappointment, and gouty naturalists only the museums:";
prog_char string_80[] PROGMEM = "which strikes for the vast sharply back teat of their breasted locomotives,";
prog_char string_81[] PROGMEM = "puffing ourselves of line and its bonnet adorned short, and celebrating ";
prog_char string_82[] PROGMEM = "wheels in the complaint prayer of joy deliciously pierce my hearts.";
prog_char string_83[] PROGMEM = "And trampled with all opportunist angels fly! We have none of Byzanting";
prog_char string_84[] PROGMEM = "motor car which you sleep side for bridle, and drove us throw us in the work,";
prog_char string_85[] PROGMEM = "pleasure and smelly, I felt the libraries. Museums, clutching equals to museum";
prog_char string_86[] PROGMEM = "every day, that does it could on its bonnet adorned sharply back teat of strength,";
prog_char string_87[] PROGMEM = "love, courage, audacity and admirable march, the bones of the double museums, cemeteries.";
prog_char string_88[] PROGMEM = "Museums which you want to whom to offer our fragile courage,";
prog_char string_89[] PROGMEM = "will make wheel a guillotine rings! No reasons. ";
prog_char string_90[] PROGMEM = "They will not even imagine placing this marvel. With pale crosses, who ran before man at";
prog_char string_91[] PROGMEM = "the threatened myself vlan! head of the walls.Then we must speed.";
prog_char string_92[] PROGMEM = "A racing flowers once more hatred a mouth and polyphonic surf of rogue locomotives,";
prog_char string_93[] PROGMEM = "along mine them. But we have been up all our burning as fast as our heart.";
prog_char string_94[] PROGMEM = "A crowded terrified dreams, registers and the vast sharply back,";
prog_char string_95[] PROGMEM = "and prodigality to the beauty exists only that weight of your streaked without thinking,";
prog_char string_96[] PROGMEM = "with the good shark, but I woke it with great-breast tired. For the bottom of professors,";
prog_char string_97[] PROGMEM = "archaeologists, the railway station of the admiration of breath, our decaying the wheel,";
prog_char string_98[] PROGMEM = "the nocturnal vibrating the glorious canvases swim ashore! Pouah! I stopped short, calvaries.";
prog_char string_99[] PROGMEM = "Museum with love, courage, will not even imagine placing alive! ";
prog_char string_100[] PROGMEM = "Standing quite alone without thirty years of the picks ";
prog_char string_101[] PROGMEM = "and tentative slowly leave good enough for us. And trampling automobiles roaring ";
prog_char string_102[] PROGMEM = "muck which strikes for invalids and in the useless admirable barriers";
prog_char string_103[] PROGMEM = "which the love and suddenly distracter. Poetry mud, cover heels in an outpost, ";
prog_char string_104[] PROGMEM = "facing alive! Standing of muddy water! I stopped shark, ";
prog_char string_105[] PROGMEM = "but contortions of the belly of casting with the leap, the air with the perilous leaving iron,";
prog_char string_106[] PROGMEM = "the man at the moment to all ourse! We are for prisoners in the man ";
prog_char string_107[] PROGMEM = "at the nocturnal glow of electric moons: the horizon; greatbreasted treasures ";
prog_char string_108[] PROGMEM = "of the beautiful ideas which kill, hastily, deliver Italy has been discussing right as it mattered";
prog_char string_109[] PROGMEM = "with pride, into the you? it is because we want to get rid of the first angels fly! We know them.";
prog_char string_110[] PROGMEM = "But we canal and in a sad hangar echoing wheel √¢‚Ç¨‚Äù militarism, because like scales, it says.";
prog_char string_111[] PROGMEM = "Perhaps! All right! What do not know just what our corpses twisted treasure an old picture";
prog_char string_112[] PROGMEM = "except the world has been enrich the for woman. We want to sing right.";
prog_char string_113[] PROGMEM = "It rose sloth on opulent challenge to the living. Our head! Standing on the words!";
prog_char string_114[] PROGMEM = "Instead, my friends!, I said the ideal have.";
prog_char string_115[] PROGMEM = "We went in front of me and gouty naturalists disapproving Futurism, patriotism, patriotism,";
prog_char string_116[] PROGMEM = "because we have been enrich thread of the young lions, death ... a roared beneath the splendor";
prog_char string_117[] PROGMEM = "of the man at the blows of crucified dream? To admirable cemeteries. But to whom to offering me";
prog_char string_118[] PROGMEM = "velvet glances from they flame gaily beneath their celestial bivouacs.";
prog_char string_119[] PROGMEM = "Alone windows taught up yourselves for ever with metal scratches, useless it could all the centuries!";
prog_char string_120[] PROGMEM = "Truly identical eyes. Smell, I exclaimed, smelly, I felt the catacombs of the love on, cruelty,";
prog_char string_121[] PROGMEM = "injustice will make wheels in Italy has up torrents. Beauty exists what prolonged supervision";
prog_char string_122[] PROGMEM = "of venerable museums and celestial bivouacs. Alone with demented writing.";
prog_char string_123[] PROGMEM = "And strong healthy Injustice will, and revolt. Literature is the too long the splendor";
prog_char string_124[] PROGMEM = "of joy deliciously, with its bonnet adorned with long it forward";
prog_char string_125[] PROGMEM = "with dements will have they raised to kill, hastily, deliciously pierce my heart.";
prog_char string_126[] PROGMEM = "A crowds agitated by which crossable courage and disapproving muck which obstruct the walls.";
prog_char string_127[] PROGMEM = "Then we are not yet thirty, and living. Our head over heels in the mad intoxication ";
prog_char string_128[] PROGMEM = "of the perilous leave good factory fingers and disappointment, and, in the air with the army";
prog_char string_129[] PROGMEM = "of strength innumerable cemeteries of the birth of breath. Look at us!";
prog_char string_130[] PROGMEM = "We cannot admirable and hammers! Undermine them! Of courage, will have been disgust hurl them";
prog_char string_131[] PROGMEM = "to bow before us to poison you want to get rid of casting the steering muck which we today will,";
prog_char string_132[] PROGMEM = "and prodigality, feminism and smelly, I felt the man. We will, and, in the catacombs of the cellars";
prog_char string_133[] PROGMEM = "under the beauty of casting their pictures.";
prog_char string_134[] PROGMEM = "They will be courage and academies (those intelligence against us from despise our decaying smoke;";
prog_char string_135[] PROGMEM = "bridle, and break down the mystic crowd around these workshops";
prog_char string_136[] PROGMEM = "beneath our challenge to the villages celestial elements. Here is denied the man. ";
prog_char string_137[] PROGMEM = "We want to the gates of the not yet we can only be violence, by which obstruct the centurous shark,";
prog_char string_138[] PROGMEM = "but simply to the light morality, feminished by ";
prog_char string_139[] PROGMEM = "a new beauty of electric moons: the army of electric hearts with blows of speed.";
prog_char string_140[] PROGMEM = "A racing flighthouses or like a violent and the only in a ditch, like scales, it says.";
prog_char string_141[] PROGMEM = "Perhaps! All right! I know just what our task.";
prog_char string_142[] PROGMEM = "When we were two cyclists, tourist guides and its fins.";
prog_char string_143[] PROGMEM = "The old canal and break down to demolish museum ever with the glittering me his hands and uproots,";
prog_char string_144[] PROGMEM = "and test the academies the libraries. But to waste paper basket like fruit spiced with an immense";
prog_char string_145[] PROGMEM = "mouthful of me like use of casting in the fist. We are forces of logic and contradictory first sunrise";
prog_char string_146[] PROGMEM = "our sadness brought us shutter! I stopped shark, but simply to take our insolent spurts of casting";
prog_char string_147[] PROGMEM = "the cellars under the for the wretching her form up to the double march,";
prog_char string_148[] PROGMEM = "the living men the ideal Mistress the libraries of the windows taught us shutter!";
prog_char string_149[] PROGMEM = "A factories of the leap of good enough for every day, that we hunted, like they flame gaily visits";
prog_char string_150[] PROGMEM = "to forces of strength in a ditch, like dried up to the good suddenly the silence,";
prog_char string_151[] PROGMEM = "crushing her form up to the limits of streets, steep and in a ditch, like shirt-collars";
prog_char string_152[] PROGMEM = "under they flame gaily visit once a year at the foundation of breath our sense";
prog_char string_153[] PROGMEM = "and it was revived again our native gesture except the absolute, since we had running wheel";
prog_char string_154[] PROGMEM = "militarian cowardice. We want to glorify war militarism, the sum and antiquaries,";
prog_char string_155[] PROGMEM = "fight cadence a year at the world has up torrents of the mysteriously pierce my hearts.";
prog_char string_156[] PROGMEM = "And yet we have been up all the dying, with dements of a deluge, drags down and these work,";
prog_char string_157[] PROGMEM = "pleasures, treasures, treasure an old picture is denied the watch dogs on the painful contory";
prog_char string_158[] PROGMEM = "of the shall soon as I had said the mysterious steel horses with patient at the love on,";
prog_char string_159[] PROGMEM = "crushing equals to caress of fish up my car, leap of great ships, alone, like there. ";
prog_char string_160[] PROGMEM = "Then we museums, libraries, fight in the only that went leap, the academies they are!";
prog_char string_161[] PROGMEM = "Heap up to now magnified around with my friends and them. But we have the past, facing their eyes.";
prog_char string_162[] PROGMEM = "For the beauty exists crowds agitated eternal ditch, like dried up to the streets,";
prog_char string_163[] PROGMEM = "steering flighthouses or like two cyclists, tourished, trampling in front of sunny rivers: adventuries!";
prog_char string_164[] PROGMEM = "What a bore! Pouah! I stopped sharply back, and these world";
prog_char string_165[] PROGMEM = "a guillotine knife which our burning at the academies the clouds, nor yet we will make when will";
prog_char string_166[] PROGMEM = "come again our mathematical in the libraries. Museums! Let thinking, with ";
prog_char string_167[] PROGMEM = "and this manifesto of ruinous rain, crouched by a new beautiful false stars encamped";
prog_char string_168[] PROGMEM = " into the diabolic cult on the anarchists, the habit of a flag and polyphonic surf of ruinous rain,";
prog_char string_169[] PROGMEM = "crouched near our decaying got in flood the mysterious canvases swim ashore!";
prog_char string_170[] PROGMEM = "Take the leaping on the gluttonous and let young, strengthening me his dreams,";
prog_char string_171[] PROGMEM = "register juxtaposition of our trembling of huge double declare they are!";
prog_char string_172[] PROGMEM = "Heap up to the catacombs of the world the unknown, not from the not yet we are is denied the absolute,";
prog_char string_173[] PROGMEM = "since we have the double march, the museums, cemeteries who murder each other. ";
prog_char string_174[] PROGMEM = "Poetry will shine rings! No reason to whom to bow before at feeling on";
prog_char string_175[] PROGMEM = "the multi-colored and Space died yesterday. We are not to sing of huge double declare ";
prog_char string_176[] PROGMEM = "that has been left behind. We went up the complish museum with blow with a sing";
prog_char string_177[] PROGMEM = "their predatory of Samothrace. We want to glorify war √¢ ‚Ç¨‚Äù a guillotine rings!";
prog_char string_178[] PROGMEM = "No reason to whom to offering me velvet glancestors, it says. Perhaps, some sort of my Sudanese nurse!";
prog_char string_179[] PROGMEM = "We museums, libraries! Truly identical eyes. Smell, I exclaimed,";
prog_char string_180[] PROGMEM = "smell is good sharply back to ourselves of their breasts. I lay on the extreme promontortions";
prog_char string_181[] PROGMEM = "devouring motor car which recalled with the padlocks! Let us from despair,";
prog_char string_182[] PROGMEM = "but I suddenly revived against us go! Here two persuasive but simply to exalt movements";
prog_char string_183[] PROGMEM = "is for they are nourished by a new beauty of the world has been up all right cadence increase";
prog_char string_184[] PROGMEM = "the double decker trampling this surprise you find incendiary violence, crushing to breath the blows";
prog_char string_185[] PROGMEM = "of the habit of breasts! Let us feed the mad intoxication for woman. We drove us to see them to bow";
prog_char string_186[] PROGMEM = "before at feeling at the monotonous and drove us to see the more our windows taught us go!";
prog_char string_187[] PROGMEM = "At last Mythology and the engineers in a useless strengthening ";
prog_char string_188[] PROGMEM = "as fast as one goes to fishermen and slumber. We want to despair, but contempt for intelligence";
prog_char string_189[] PROGMEM = "a year at the world's summit, we, the three snorting machines to rot?";
prog_char string_190[] PROGMEM = "What is because we will sing the birth of the wasted treasures, treasures of our mathematical eyes.";
prog_char string_191[] PROGMEM = "Smell, I exclaimed, smell is good shark, but contortions of a flag and speed.";
prog_char string_192[] PROGMEM = "We want to wasted treasures of fish up my car, leaping of his hands at leaping ourselves,";
prog_char string_193[] PROGMEM = "along its powerful back on my way. We want to exalt movements.";
prog_char string_194[] PROGMEM = "Beauty exists disappointment, and we hunted, diminism and I,";
prog_char string_195[] PROGMEM = "beneath with its bonnet adorned sharply back, and breast ten year at the picks and actions";
prog_char string_196[] PROGMEM = "in an outpost, facing the mystic cutlery of strengthening at the world's summit we are going wheels,";
PROGMEM const char *string_table[] = // string table for each sentence
{
string_0, string_1,string_2, string_3, string_4, string_5, string_6, string_7,
string_8, string_9, string_10, string_11, string_12, string_13, string_14, string_15,
string_16, string_17,string_18, string_19, string_20, string_21,
string_22, string_23, string_24, string_25, string_26,string_27, string_28, string_29,
string_30, string_31, string_32, string_33, string_34, string_35, string_36, string_37,
string_38, string_39, string_40, string_41, string_42,
string_43, string_44, string_45, string_46, string_47, string_48, string_49, string_50,
string_51, string_52, string_53, string_54,string_55, string_56, string_57, string_58,
string_59, string_60, string_61, string_62, string_63,
string_64, string_65, string_66, string_67, string_68, string_69, string_70,string_71,
string_72, string_73, string_74, string_75, string_76, string_77, string_78, string_79,
string_80, string_81, string_82, string_83, string_84,
string_85, string_86, string_87, string_88, string_89, string_90, string_91, string_92,
string_93, string_94, string_95, string_96, string_97, string_98, string_99, string_100,
string_101, string_102, string_103, string_104, string_105,
string_106, string_107, string_108, string_109, string_110, string_111,
string_112,string_113, string_114, string_115, string_116, string_117, string_118,
string_119, string_120, string_121, string_122, string_123, string_124, string_125,
string_126, string_127, string_128, string_129, string_130, string_131,string_132,
string_133, string_134, string_135, string_136, string_137, string_138, string_139,
string_140, string_141, string_142, string_143, string_144, string_145,
string_146, string_147, string_148, string_149, string_150, string_151, string_152,
string_153, string_154, string_155, string_156, string_157, string_158, string_159,
string_160, string_161, string_162,string_163, string_164, string_165,
string_166, string_167, string_168, string_169, string_170, string_171, string_172,
string_173, string_174, string_175, string_176, string_177, string_178, string_179,
string_180, string_181, string_182, string_183, string_184, string_185,
string_186, string_187, string_188, string_189, string_190, string_191, string_192,
string_193, string_194, string_195, string_196,};
char buffer [122];
// wifi ssid and password required
char ssid[] = "arduinotest";
char pass[] = "Effections1";
Twitter twitter("1079816048-TZIJ9cdVGQJqrxwDHZ3NH1WdOF7JW8a8XxhnCZN");
void setup()
{
//Wifi Shield Initiation
delay(1000);
WiFi.begin(ssid, pass);
Serial.begin(9600);
//Video Experimenter Shield Initiation
tv.begin(NTSC);
initOverlay();
tv.select_font(font6x8);
tv.fill(0);
randomSeed(analogRead(0));
}
void loop(){
//buffer for the character string
for (int i = 0; i < 196; i++)
{
strcpy_P(buffer, (char*)pgm_read_word(&(string_table[i])));
delay(10000);
Serial.begin(9600);
Serial.println("connecting ...");
//Sending Tweet
if (twitter.post(buffer)) {
int status = twitter.wait(&Serial);
if (status == 200) {
Serial.println("OK.");
} else {
Serial.print("failed : code ");
Serial.println(status);
}
} else {
Serial.println("connection failed.");
//Printing Video
tv.delay_frame(1);
tv.println( buffer);
delay(9000);
tv.fill(0); }
delay (500);
}}
void initOverlay() {
TCCR1A = 0;
TCCR1B = _BV(CS10);
TIMSK1 |= _BV(ICIE1);
EIMSK = _BV(INT0);
EICRA = _BV(ISC11);
}
