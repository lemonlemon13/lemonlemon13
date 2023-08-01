import os
import math

#PRNG
class PRNG:
    def __init__(self, seed: int) -> None:
        self._seed = seed
        self._out = seed
    
    def next(self, min: int, max: int) -> int:
        """ Generates the next random number in the sequence
            between min (inclusive) and max (exclusive)
        """
        if self._out == 1:
            self._seed = (self._seed % 1000000) + 1
            self._out = self._seed
            return 1
        elif self._out % 2 == 0:
            self._out //= 2
        else:
            self._out = 3 * self._out + 1
        return ((self._out + self.next(min, max)) % (max-min+1)) + min

cur_system = ''
cur_planet = ''
cur_continent = ''
cur_country = ''
cur_state = ''
cur_town = ''
game_seed = 2 #Must be 2 or higher
party_size = 1 #Must be 1 or higher
level = 1 
exp = 0
xp_to_level = [300, 900, 2700, 6500, 14000,
               23000, 34000, 48000, 64000, 85000,
               100000, 120000, 140000, 165000, 195000,
               225000, 265000, 305000, 355000, 500000]
cr_xp = {0.0:10, 0.125:25, 0.25:50, 0.5:100, 1.0:200,
         2.0:450, 3.0:700, 4.0:1100, 5.0:1800, 6.0:2300,
         7.0:2900, 8.0:3900, 9.0:5000, 10.0:5900, 11.0:7200,
         12.0:8400, 13.0:10000, 14.0:11500, 15.0:13000, 16.0:15000,
         17.0:18000, 18.0:20000, 19.0:22000, 20.0:25000, 21.0:33000,
         22.0:41000, 23.0:50000, 24.0:62000, 25.0:75000, 26.0:90000,
         27.0:105000, 28.0:120000, 29.0:135000, 30.0:155000}
governments = ["Anarchy", "Democracy", "Autocracy", "Monarchy", "Theocracy", "Military Dictatorship"]
places = ["Guildhall", "Mine", "Tavern", "Temple", "Store"]
gender = ['Male', 'Female', 'Intersex', 'Non-Binary']
story = ['Gain Wealth', 'Slay the Beast', 'Religious Journey', 'MacGuffin Quest', 'Noble Tragedy', 'haha funny jOke']

def GenName() -> str:
    syll_count = rng.next(2, 4)
    name = ''
    for x in range(1, syll_count):
        name += syllables[rng.next(0, len(syllables)-1)]
    return name.title()

def GenSystem(a) -> None:
    planets = rng.next(1, 12)
    bingus = 0
    system_name = GenName()
    for b in range(1, planets):
        #Planet Gen
        bingus = GenPlanet(a, b, bingus, system_name)

def GenPlanet(a, b, last_au, system_name) -> float:
    au = 0.1 * rng.next(1, 50) + last_au
    planet_name = GenName()
    if not (0.8 <= au and au <= 1.2):
        #Uninhabitable Planet
        adv.write(f'Act: The {system_name} System - ' + str(a) + f' Scene: {planet_name} - ' + str(b) + '\n')
        adv.write(f'{planet_name} is uninhabited but contains:\n')
        ore_in = []
        ore_no = rng.next(1, len(ores)//2)
        for x in range(ore_no):
            put_in = ores[rng.next(0, len(ores)-1)]
            if put_in not in ore_in:
                ore_in.append(put_in)
                adv.write(put_in + '\n')
        adv.write('\n')
    else:
        #Habitable Planet Gen (0.8-1.2AU)
        continents = rng.next(1, 12)
        for c in range(1, continents):
        #Continent Gen
            GenContinent(a, b, c, system_name, planet_name)
    return au

def GenContinent(a, b, c, system_name, planet_name) -> None:
    continent_name = GenName()
    countries = rng.next(1, 30)
    for d in range(1, countries):
        #Country Gen
        GenCountry(a, b, c, d, system_name, planet_name, continent_name)

def GenCountry(a, b, c, d, system_name, planet_name, continent_name) -> None:
    country_name = GenName()
    biome = biomes[rng.next(0, len(biomes)-1)]
    states = rng.next(1, 20)
    for e in range(1, states):
        #State Gen
        GenState(a, b, c, d, e, system_name, planet_name, continent_name, country_name, biome)

def GenState(a, b, c, d, e, system_name, planet_name, continent_name, country_name, biome) -> None:
    state_name = GenName()
    towns = rng.next(1, 7)
    for f in range(1, towns):
        #Town Gen
        GenTown(a, b, c, d, e, f, system_name, planet_name, continent_name, country_name, state_name, biome)

def GenTown(a, b, c, d, e, f, system_name, planet_name, continent_name, country_name, state_name, biome) -> None:
    #   Story Title
    town_name = GenName()
    adv.write(f'Act: The {system_name} System - ' + str(a) + 
              f' Scene: {planet_name} - ' + str(b) + 
              f' Part: {continent_name} - ' + str(c) + 
              f' Section: {country_name} - ' + str(d) + 
              f' Chapter: {state_name} State - ' + str(e) + 
              f' Episode: {town_name} Town - ' + str(f) + '\n')
    #   Approach (Road)
    GenRoad(biome, town_name)
    #   Town Details
    adv.write(f'- The Town -\nName: {town_name}\n')
    #       Population
    #       Race Split
    adv.write(f'Population: {rng.next(100, 100000)}, mostly {races[rng.next(0, len(races)-1)]}\n')
    #       Government
    global governments
    adv.write(f'Government: {governments[rng.next(0, len(governments)-1)]}\n')
    for g in range(1, rng.next(1, 12)):
    #       Districts
        GenDistrict()
    #   Set Up (Start Story)
    adv.write(f'\n- Story Archetype -\n{story[rng.next(0, len(story)-1)]}\n')
    for j in range(1, rng.next(1, 3)):
        GenDungeon(j)
    #   Story Point (Dungeon/Cave/POI)
    adv.write(f'The Party leave {town_name}.\n\n')
    #   Leaving (End Story)

def GenDistrict() -> None:
    # Name
    adv.write(f'District: - {GenName()} -\nNotable Places:\n')
    for h in range(1, rng.next(2, 3)):
    #           Points of Interest
        GenPOI()
    adv.write("Some NPCs:\n")
    for i in range(1, rng.next(3, 6)):
    #           Some NPCs
        GenNPC()

def GenDungeon(num: int) -> None:
    global level
    if level > 20:
        adv.write(f'- Story Point {num} -\nGenerate at Level 20\n')
    else:
        adv.write(f'- Story Point {num} -\nGenerate at Level {level}\n')
    encounters = rng.next(3, 10)
    for x in range(encounters):
        adv.write(f'Encounter {x+1}:\n')
        GenEncoutner()

def GenRoad(curbiome, town_name) -> None:
    adv.write(f'Biome: {curbiome}\n- The Approach -\n')
    #Possible Encounter
    if (rng.next(1, 2) == 1):
        GenEncoutner()
    else:
        adv.write('Nothing Eventful Happens.\n')
    adv.write(f'The Party Arrives at {town_name}.\n\n')

def GenPOI() -> None:
    global places
    adv.write(f"\tThe {GenName()} {places[rng.next(0, len(places)-1)]}\n")

def GenNPC() -> None:
    #               Name
    #               Gender
    #               Race
    #               Class
    #               Age
    adv.write(f"Name: {GenName()}\n"+
              f"\tGender: {gender[rng.next(0, len(gender)-1)]}\n"+
              f"\tRace: {races[rng.next(0, len(races)-1)]}\n"+
              f"\tClass: {classes[rng.next(0, len(classes)-1)]}\n"+
              f"\tAge: {rng.next(20, 65)}\n")

def GenEncoutner() -> None:
    global party_size
    global level
    global exp
    global xp_to_level
    global cr_xp
    #Find CR of Encoutner
    cr_target = 0
    en_mon = []
    if level <= 2:
        cr_target = (math.log((party_size+1), 3) * level + rng.next(1, 3) - 2) / 4
    else:
        cr_target = math.log((party_size+1), 3) * (level - 2) + rng.next(1, 3) - 2
    #Generate Monsters to match CR
    cr_curr = 0
    en_xp = 0
    while cr_curr < cr_target:
        temp_mon = monsters[rng.next(0, len(monsters)-1)]
        mon_name = temp_mon[0]
        mon_cr = temp_mon[1]
        if mon_cr > (cr_target - cr_curr):
            continue
        else:
            if mon_cr == 0:
                mon_cr = 0.75
            ### DEBUG PRINT ###
            mon_count = rng.next(1, int((cr_target-cr_curr)/mon_cr) + 1)
            if mon_cr == 0.75:
                mon_cr = 0
            cr_curr += mon_cr * mon_count + 0.125
            en_mon.append((mon_name, mon_count))
            #Add XP to Players
            exp += cr_xp[mon_cr] * mon_count
            en_xp += cr_xp[mon_cr] * mon_count
            #Level Up
            if level < 20:
                to_up = xp_to_level[level-1]
            else:
                to_up = 500000 + ((level-19) ^ 2) * 10000
            while exp >= to_up:
                level += 1
                exp -= to_up
    adv.write(f'ENCOUNTER - CR: {cr_target:.3}\n')
    for mon in en_mon:
        adv.write(f'{mon[1]}x {mon[0]}\n')
    adv.write(f'Gained {en_xp}exp\nPlayers at Level {level}: {exp}exp\n')

def LoadTables() -> None:
    #Load Class Table
    global classes
    classes = []
    file = open('Tables/Base/Class.txt', 'r')
    counter = 0
    for line in file:
        classes.append(line.removesuffix('\n'))
        counter += 1
        print(f'Class {counter}  of 13 Loaded')
    classes.sort()
    #Load Monster Table
    global monsters
    monsters = []
    file = open('Tables/Base/Monster.txt', 'r')
    counter = 0
    for line in file:
        mon = line.split(' ')
        mon[1] = float(mon[1])
        monsters.append(tuple(mon))
        counter += 1
        print(f'Monster {counter} of 780 Loaded')
    monsters.sort()
    #Load Race Table
    global races
    races = []
    file = open('Tables/Base/Race.txt', 'r')
    counter = 0
    for line in file:
        races.append(line.removesuffix('\n'))
        counter += 1
        print(f'Race {counter} of 48 Loaded')
    races.sort()
    #Load Syllable Table
    global syllables
    syllables = []
    counter = 0
    file = open('Tables/Base/Syllable.txt', 'r')
    for line in file:
        syllables.append(line.removesuffix('\n'))
        counter += 1
        print(f'Syllable {counter} of 2014 Loaded')
    #Load Ores Table
    global ores
    ores = []
    file = open('Tables/Base/Ores.txt', 'r')
    counter = 0
    for line in file:
        ores.append(line.removesuffix('\n'))
        counter += 1
        print(f'Ore {counter} of 20 Loaded')
    file.close()
    #Load Biome Table
    global biomes
    biomes = []
    file = open('Tables/Base/Biome.txt', 'r')
    counter = 0
    for line in file:
        biomes.append(line.removesuffix('\n'))
        counter += 1
        print(f'Biome {counter} of 15 Loaded')
    file.close()

rng = PRNG(game_seed)
LoadTables()
#Title
adv = open('GalAdv.txt', 'w')
adv.write('Dungeons and Dragons: Galatic Adventure\nVolume 1: Acts 1-100\n\n')
#Prologue (Human Written)
adv.write('PROLOGUE\n[something human written]\n\n')
#Galaxy Gen
for a in range(1, 101):
    #Star System Gen
    GenSystem(a)
    print('Story Progress', f' {(a-1)/100:3.0%} [' + ('▒' * ((a-1)//4) + (' ' * (25-((a-1)//4))) + ']'))
#Finale/Epilogue (Human Written)
#Extra Content
adv.write('Additional Notes:\n'+
'Post Lv. 20 Rewards:\n'+
"Called Beyond Levels, Players gain a Beyond Level for every 500'000XP past Level 20\n"+
'and can redeem the following:\n'+
'. Ability Score Improvement\n'+
'  - Improve 1 Ability Score by 2 or 2 Scores by 1\n'+
'. Advanced Mastery\n'+
'  - Improve Proficiency Bonus by 1\n'+
'. Specified Resilience\n'+
'  - Gain Resistance to 1 Damage Type\n'+
'. Specified Resilience +\n'+
'  - Gain Immunity to 1 Damage Type (must have at least 1 Damage Type not immune to)\n'+
'    Requires Specified Resilience to the Damage Type\n'+
'. Class Expertise\n'+
'  - Enhancement of 1 Class Trait of Choice\n'+
'    (Discuss with DM)\n'+
'. Evolution of Self\n'+
'  - Enhancement of 1 Racial Trait of Choice\n'+
'    (Discuss with DM)\n'+
'. Spellcasting Advancement\n'+
'  - Gain 1 Spell Slot of Any Level (besides Level 9)\n'+
'    (Discuss with DM)')
#CleanUp
adv.close()
print('Story Progress', '100% [' + ('▒' * 25) + ']')