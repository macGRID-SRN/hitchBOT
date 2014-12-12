var flightPlanCoordinates = [ 
 new google.maps.LatLng(44.64639727,-63.58323981),
 new google.maps.LatLng(44.96898787,-63.51787814),
 new google.maps.LatLng(45.87159308,-64.28160716),
 new google.maps.LatLng(45.87193267,-64.28095969),
 new google.maps.LatLng(45.87242379,-64.28072511),
 new google.maps.LatLng(45.87207019,-64.28083536),
 new google.maps.LatLng(45.87206496,-64.28083301),
 new google.maps.LatLng(45.87200536,-64.28080865),
 new google.maps.LatLng(46.17857765,-64.61387391),
 new google.maps.LatLng(46.82969315,-64.93533798),
 new google.maps.LatLng(48.01743936,-66.37443042),
 new google.maps.LatLng(48.00880813,-66.67020595),
 new google.maps.LatLng(45.40765163,-73.94978659),
 new google.maps.LatLng(44.27559979,-76.87067344),
 new google.maps.LatLng(44.27495935,-76.87660276),
 new google.maps.LatLng(44.2742464,-76.883202),
 new google.maps.LatLng(44.27362441,-76.88909794),
 new google.maps.LatLng(44.27316459,-76.89551352),
 new google.maps.LatLng(43.6753439,-79.34947191),
 new google.maps.LatLng(43.66021317,-79.43505229),
 new google.maps.LatLng(43.23248279,-79.90288505),
 new google.maps.LatLng(43.232414,-79.9029322),
 new google.maps.LatLng(43.2324158,-79.9029299),
 new google.maps.LatLng(43.2324488,-79.9028878),
 new google.maps.LatLng(43.2423238,-79.839161),
 new google.maps.LatLng(43.2423432,-79.8391485),
 new google.maps.LatLng(43.232515,-79.9028616),
 new google.maps.LatLng(43.2324133,-79.9029021),
 new google.maps.LatLng(43.5440053,-80.287367),
 new google.maps.LatLng(44.9661215,-81.2752133),
 new google.maps.LatLng(45.254509,-81.6649039),
 new google.maps.LatLng(45.7399981,-82.0826266),
 new google.maps.LatLng(45.8116374,-81.7115472),
 new google.maps.LatLng(45.8116377,-81.7115459),
 new google.maps.LatLng(45.8116376,-81.7115472),
 new google.maps.LatLng(45.8148256,-81.6924777),
 new google.maps.LatLng(45.8148243,-81.692479),
 new google.maps.LatLng(45.8148243,-81.692479),
 new google.maps.LatLng(45.8148247,-81.692479),
 new google.maps.LatLng(45.7686666,-81.7617087),
 new google.maps.LatLng(45.7686669,-81.7617083),
 new google.maps.LatLng(45.7686666,-81.7617091),
 new google.maps.LatLng(45.7423322,-81.7818659),
 new google.maps.LatLng(45.7677626,-81.7639097),
 new google.maps.LatLng(45.7677626,-81.7639093),
 new google.maps.LatLng(46.508856,-84.3290997),
 new google.maps.LatLng(48.4370889,-89.2160178),
 new google.maps.LatLng(50.4434473,-104.5483632),
 new google.maps.LatLng(51.27404455,-117.07831732),
 new google.maps.LatLng(50.1702233,-119.4450214),
 new google.maps.LatLng(50.23414571,-119.36003018),
 new google.maps.LatLng(50.23383628,-119.35947784),
 new google.maps.LatLng(50.23332124,-119.35809136),
 new google.maps.LatLng(50.23144868,-119.35806114),
 new google.maps.LatLng(50.2310041,-119.3560925),
 new google.maps.LatLng(50.2337099,-119.3584627),
 new google.maps.LatLng(50.23113064,-119.350389),
 new google.maps.LatLng(50.23107101,-119.34616583),
 new google.maps.LatLng(49.886404,-119.4970016),
 new google.maps.LatLng(49.8622588,-119.370444),
 new google.maps.LatLng(49.8622586,-119.3704457),
 new google.maps.LatLng(49.862259,-119.3704457),
 new google.maps.LatLng(49.8623211,-119.3704761),
 new google.maps.LatLng(49.8622588,-119.3704457),
 new google.maps.LatLng(49.8622995,-119.37047),
 new google.maps.LatLng(49.8622586,-119.3704461),
 new google.maps.LatLng(49.88638291,-119.49727353),
 new google.maps.LatLng(49.8622588,-119.370444),
 new google.maps.LatLng(49.860668,-119.3704643),
 new google.maps.LatLng(49.8622867,-119.3703485),
 new google.maps.LatLng(49.8825504,-119.4332066),
 new google.maps.LatLng(49.8622563,-119.3702929),
 new google.maps.LatLng(49.8622227,-119.370265),
 new google.maps.LatLng(49.8622567,-119.3702898),
 new google.maps.LatLng(49.8622565,-119.3702914),
 new google.maps.LatLng(49.8622567,-119.3702898),
 new google.maps.LatLng(49.8622563,-119.3702928),
 new google.maps.LatLng(49.8457483,-119.4867637),
 new google.maps.LatLng(49.8622208,-119.370268),
 new google.maps.LatLng(49.8622565,-119.3702914),
 new google.maps.LatLng(49.8623299,-119.3704158),
 new google.maps.LatLng(49.86233,-119.3704144),
 new google.maps.LatLng(49.8623291,-119.3704154),
 new google.maps.LatLng(49.8623291,-119.3704154),
 new google.maps.LatLng(49.8623299,-119.3704158),
 new google.maps.LatLng(49.8623291,-119.3704154),
 new google.maps.LatLng(49.8623299,-119.3704158),
 new google.maps.LatLng(49.8623299,-119.3704158),
 new google.maps.LatLng(49.8623299,-119.3704158),
 new google.maps.LatLng(49.86233,-119.3704144),
 new google.maps.LatLng(49.8623299,-119.3704158),
 new google.maps.LatLng(49.86233,-119.3704144),
 new google.maps.LatLng(49.8623299,-119.3704158),
 new google.maps.LatLng(49.862244,-119.3704066),
 new google.maps.LatLng(49.848697,-119.5991737),
 new google.maps.LatLng(49.50371,-119.5946708),
 new google.maps.LatLng(49.4762889,-119.6074476),
 new google.maps.LatLng(49.4761929,-119.6074573),
 new google.maps.LatLng(49.4762895,-119.6074447),
 new google.maps.LatLng(49.478952,-119.5985701),
 new google.maps.LatLng(49.4760806,-119.6073285),
 new google.maps.LatLng(49.4906937,-119.6044479),
 new google.maps.LatLng(49.5013065,-119.6038633),
 new google.maps.LatLng(49.4761599,-119.6073472),
 new google.maps.LatLng(49.4761425,-119.607343),
 new google.maps.LatLng(49.4761363,-119.6073443),
 new google.maps.LatLng(49.4761353,-119.6073342),
 new google.maps.LatLng(49.476049,-119.6073079),
 new google.maps.LatLng(49.4760601,-119.6074191),
 new google.maps.LatLng(49.4758933,-119.6073221),
 new google.maps.LatLng(49.4761631,-119.6073504),
 new google.maps.LatLng(49.4761685,-119.6073486),
 new google.maps.LatLng(49.21787286,-119.91966769),
 new google.maps.LatLng(49.22201842,-119.95797001),
 new google.maps.LatLng(49.0498756,-122.2024161),
 new google.maps.LatLng(49.2756848,-123.2146149),
 new google.maps.LatLng(48.94089967,-123.22347886),
 new google.maps.LatLng(48.6634017,-123.4163147),
 new google.maps.LatLng(48.6635424,-123.416459),
 new google.maps.LatLng(48.6634748,-123.4164208),
 new google.maps.LatLng(48.6634291,-123.4164511),
 new google.maps.LatLng(48.6633938,-123.4164993),
 new google.maps.LatLng(48.6634658,-123.416522),
 new google.maps.LatLng(48.6633743,-123.4166003),
 new google.maps.LatLng(48.6634072,-123.4165825),
 new google.maps.LatLng(48.6633468,-123.4166255),
 new google.maps.LatLng(48.6634683,-123.4165221),
 new google.maps.LatLng(48.4143355,-123.3789099),
 new google.maps.LatLng(48.4142628,-123.3790363),
 new google.maps.LatLng(48.4257183,-123.35869),
 new google.maps.LatLng(48.4142398,-123.3791333),
 new google.maps.LatLng(48.414436,-123.378862),
 new google.maps.LatLng(48.4143664,-123.3785835),
 new google.maps.LatLng(48.4146712,-123.3788402),
 new google.maps.LatLng(48.4144026,-123.3789218),
 new google.maps.LatLng(48.4145218,-123.3788636),
 new google.maps.LatLng(48.4145786,-123.3788557),
 new google.maps.LatLng(48.5996545,-123.3960841),
 new google.maps.LatLng(48.4219003,-123.3693136),
 new google.maps.LatLng(48.4143903,-123.3789689),
 new google.maps.LatLng(48.4252511,-123.369472),
 new google.maps.LatLng(48.4284211,-123.369715),
 new google.maps.LatLng(48.4243047,-123.3679583),
 new google.maps.LatLng(48.414336,-123.3788471),
 new google.maps.LatLng(48.4143493,-123.3788466),
 new google.maps.LatLng(48.4144333,-123.3790689),
 new google.maps.LatLng(48.4143658,-123.3790294),
 new google.maps.LatLng(48.4143796,-123.3787766),
 new google.maps.LatLng(48.4160476,-123.3741029),
 new google.maps.LatLng(48.4208075,-123.3259234),
 new google.maps.LatLng(48.413611,-123.3806493),
 new google.maps.LatLng(48.414266,-123.3794291),
 new google.maps.LatLng(48.4144487,-123.3793416),
 new google.maps.LatLng(48.414344,-123.3790898),
 new google.maps.LatLng(48.4339106,-123.3333209),
 new google.maps.LatLng(48.4314484,-123.3295649),
 new google.maps.LatLng(48.4341892,-123.3280866),
 new google.maps.LatLng(48.4314325,-123.3295415),
 new google.maps.LatLng(48.4252126,-123.3688827),
 new google.maps.LatLng(48.425196,-123.3688641),
 new google.maps.LatLng(48.4269752,-123.3690283),
 new google.maps.LatLng(48.4252114,-123.3687986),
 new google.maps.LatLng(48.4252283,-123.3688335),
 new google.maps.LatLng(48.4252237,-123.3688148),
 new google.maps.LatLng(48.4252321,-123.3688237),
 new google.maps.LatLng(48.4252251,-123.3688635),
 new google.maps.LatLng(48.425195,-123.3688364),
 new google.maps.LatLng(48.4252124,-123.3688089),
 new google.maps.LatLng(48.4251934,-123.3688054),
 new google.maps.LatLng(48.4252067,-123.3688282),
 new google.maps.LatLng(48.42522,-123.3688169),
 new google.maps.LatLng(48.4252218,-123.3688409),
 new google.maps.LatLng(48.4252111,-123.3688346),
 new google.maps.LatLng(48.4251995,-123.3688244),
 new google.maps.LatLng(48.4252136,-123.3688169),
 new google.maps.LatLng(48.4252255,-123.3688307),
 new google.maps.LatLng(48.4252239,-123.368824),
 new google.maps.LatLng(48.4493535,-123.3438039),
 new google.maps.LatLng(48.4204219,-123.3626171),
 new google.maps.LatLng(48.4204356,-123.3626401),
 new google.maps.LatLng(48.4198572,-123.3677801),
 new google.maps.LatLng(48.4203818,-123.3626228),
 new google.maps.LatLng(48.4259959,-123.3702347),
 new google.maps.LatLng(48.426,-123.3712964),
 new google.maps.LatLng(48.4396144,-123.3525108),
 new google.maps.LatLng(48.4395967,-123.3525126),
 new google.maps.LatLng(48.4395736,-123.3524785),
 new google.maps.LatLng(48.4395491,-123.3523426),
 new google.maps.LatLng(48.4395787,-123.3524774),
 new google.maps.LatLng(48.4395931,-123.3525194),
 new google.maps.LatLng(48.4395804,-123.3524753),
 new google.maps.LatLng(48.4395807,-123.3524849),
 new google.maps.LatLng(48.4359287,-123.3520696),
 new google.maps.LatLng(48.6107697,-123.4019465),
 new google.maps.LatLng(48.4395558,-123.3524428),
 new google.maps.LatLng(48.4395661,-123.3524757),
 new google.maps.LatLng(48.4395501,-123.3524848),
 new google.maps.LatLng(48.4395578,-123.3523311),
 new google.maps.LatLng(48.4297026,-123.3618149),
 new google.maps.LatLng(48.424952,-123.3676381),
 new google.maps.LatLng(48.4249533,-123.3676395),
 new google.maps.LatLng(48.4395761,-123.3524848),
 new google.maps.LatLng(48.4294627,-123.3573785),
 new google.maps.LatLng(48.4294585,-123.357267),
 new google.maps.LatLng(48.4395637,-123.3524799),
 new google.maps.LatLng(48.4395862,-123.3524691),
 new google.maps.LatLng(48.4395624,-123.3524568),
 new google.maps.LatLng(48.422139,-123.347672),
 new google.maps.LatLng(48.4220504,-123.3478693),
 new google.maps.LatLng(48.4220631,-123.3479422),
 new google.maps.LatLng(48.4221121,-123.3477787),
 new google.maps.LatLng(48.4221096,-123.3478546),
 new google.maps.LatLng(48.4221131,-123.3477902),
 new google.maps.LatLng(48.4221587,-123.3477348),
 new google.maps.LatLng(48.4221176,-123.3477831),
 new google.maps.LatLng(48.4221038,-123.3477734),
 new google.maps.LatLng(48.4222324,-123.3476747),
 new google.maps.LatLng(48.4221553,-123.3477074),
 new google.maps.LatLng(48.4221176,-123.3477825),
 new google.maps.LatLng(48.4221813,-123.3477587),
 new google.maps.LatLng(48.4221553,-123.3477427),
 new google.maps.LatLng(48.4221087,-123.3477908),
 new google.maps.LatLng(48.4221922,-123.3476972),
 new google.maps.LatLng(48.4220778,-123.34779),
 new google.maps.LatLng(48.4221289,-123.3476996),
 new google.maps.LatLng(48.4220891,-123.3477445),
 new google.maps.LatLng(48.4220539,-123.3477674),
 new google.maps.LatLng(48.4220738,-123.3477865),
 new google.maps.LatLng(48.4221646,-123.3477163),
 new google.maps.LatLng(48.4221559,-123.3477102),
 new google.maps.LatLng(48.4220696,-123.3477525),
 new google.maps.LatLng(48.4221189,-123.3477599),
 new google.maps.LatLng(48.4220975,-123.3477514),
 new google.maps.LatLng(48.4220843,-123.3477747),
 new google.maps.LatLng(48.4221892,-123.3477115),
 new google.maps.LatLng(48.4221199,-123.3477564),
 new google.maps.LatLng(48.4221072,-123.3477573),
 new google.maps.LatLng(48.4221748,-123.3476917),
 new google.maps.LatLng(48.4221414,-123.3477152),
 new google.maps.LatLng(48.4222774,-123.3476496),
 new google.maps.LatLng(48.4220875,-123.347828),
 new google.maps.LatLng(48.4220747,-123.3477978),
 new google.maps.LatLng(48.4220765,-123.3478419),
 new google.maps.LatLng(48.4220757,-123.3478291),
 new google.maps.LatLng(48.4221368,-123.3477066),
 new google.maps.LatLng(48.4221711,-123.3476884),
 new google.maps.LatLng(48.4220979,-123.3478049),
 new google.maps.LatLng(48.4220878,-123.3478213),
 new google.maps.LatLng(48.4220812,-123.3477942),
 new google.maps.LatLng(48.4220892,-123.3478171),
 new google.maps.LatLng(48.4220738,-123.3478088),
 new google.maps.LatLng(48.4221414,-123.3476968),
 new google.maps.LatLng(48.4220762,-123.3477521),
 new google.maps.LatLng(48.4221146,-123.3477537),
 new google.maps.LatLng(48.4221514,-123.3477101),
 new google.maps.LatLng(48.422084,-123.3477658),
 new google.maps.LatLng(48.4221704,-123.3476857),
 new google.maps.LatLng(48.422101,-123.3477129),
 new google.maps.LatLng(48.4221351,-123.3476883),
 new google.maps.LatLng(48.4221328,-123.3477168),
 new google.maps.LatLng(48.4221535,-123.3476411),
 new google.maps.LatLng(48.422096,-123.3476889),
 new google.maps.LatLng(48.4222256,-123.3476197),
 new google.maps.LatLng(48.422132,-123.3477024),
 new google.maps.LatLng(48.4221013,-123.3477169),
 new google.maps.LatLng(48.4222356,-123.347605),
 new google.maps.LatLng(48.4222121,-123.3476121),
 new google.maps.LatLng(48.4221339,-123.347693),
 new google.maps.LatLng(48.4222594,-123.3475974),
 new google.maps.LatLng(48.422076,-123.347722),
 new google.maps.LatLng(48.4221107,-123.3476888),
 new google.maps.LatLng(48.4221638,-123.3477087),
 new google.maps.LatLng(48.4221197,-123.3477005),
 new google.maps.LatLng(48.4221367,-123.347668),
 new google.maps.LatLng(48.4222961,-123.3475525),
 new google.maps.LatLng(48.4220775,-123.3477388),
 new google.maps.LatLng(48.4221769,-123.3476426),
 new google.maps.LatLng(48.4221262,-123.3476711),
 new google.maps.LatLng(48.4220784,-123.3477332),
 new google.maps.LatLng(48.4220593,-123.3477344),
 new google.maps.LatLng(48.42215,-123.3476804),
 new google.maps.LatLng(48.4221168,-123.3476805),
 new google.maps.LatLng(48.4220499,-123.3477479),
 new google.maps.LatLng(48.422198,-123.3476167),
 new google.maps.LatLng(48.4222224,-123.3476008),
 new google.maps.LatLng(48.4221952,-123.3476398),
 new google.maps.LatLng(48.422214,-123.3476179),
 new google.maps.LatLng(48.422123,-123.3476644),
 new google.maps.LatLng(48.4220663,-123.3477474),
 new google.maps.LatLng(48.4221886,-123.3476371),
 new google.maps.LatLng(48.4220815,-123.3477463),
 new google.maps.LatLng(48.4221507,-123.3476673),
 new google.maps.LatLng(48.4221406,-123.3476786),
 new google.maps.LatLng(48.4220855,-123.347727),
 new google.maps.LatLng(48.4221076,-123.3476885),
 new google.maps.LatLng(48.422077,-123.3477282),
 new google.maps.LatLng(48.4221666,-123.3476682),
 new google.maps.LatLng(48.4222318,-123.3476221),
 new google.maps.LatLng(48.4221331,-123.3476925),
 new google.maps.LatLng(48.4221729,-123.3476486),
 new google.maps.LatLng(48.4222565,-123.3475609),
 new google.maps.LatLng(48.4220935,-123.3477349),
 new google.maps.LatLng(48.4221299,-123.3476707),
 new google.maps.LatLng(48.4221796,-123.3476233),
 new google.maps.LatLng(48.4221829,-123.3476347),
 new google.maps.LatLng(48.4222341,-123.3476269),
 new google.maps.LatLng(48.4220376,-123.3477706),
 new google.maps.LatLng(48.4221145,-123.3477381),
 new google.maps.LatLng(48.4222552,-123.3476257),
 new google.maps.LatLng(48.4221943,-123.3476363),
 new google.maps.LatLng(48.4221491,-123.3476621),
 new google.maps.LatLng(48.4222045,-123.3476456),
 new google.maps.LatLng(48.4220192,-123.3477063),
 new google.maps.LatLng(48.4221963,-123.3476524),
 new google.maps.LatLng(48.4222502,-123.3476008),
 new google.maps.LatLng(48.4220434,-123.3477051),
 new google.maps.LatLng(48.4220406,-123.3477426),
 new google.maps.LatLng(48.42205,-123.3477634),
 new google.maps.LatLng(48.4220184,-123.3477409),
 new google.maps.LatLng(48.4221539,-123.3476602),
 new google.maps.LatLng(48.4220191,-123.3477036),
 new google.maps.LatLng(48.422065,-123.3476921),
 new google.maps.LatLng(48.4220588,-123.3477498),
 new google.maps.LatLng(48.4221603,-123.3476394),
 new google.maps.LatLng(48.4239957,-123.3708916),
 new google.maps.LatLng(48.4236907,-123.371013),
 new google.maps.LatLng(49.1782253,-123.1654813),
 new google.maps.LatLng(49.17821,-123.1655018),
 new google.maps.LatLng(49.1782223,-123.1654872),
 new google.maps.LatLng(49.1782257,-123.165482),
 new google.maps.LatLng(49.2903041,-123.1178106),
 new google.maps.LatLng(49.2903601,-123.1178329),
 new google.maps.LatLng(49.2015007,-122.9112913),
 new google.maps.LatLng(49.2016028,-122.9111405),
 new google.maps.LatLng(49.2044068,-122.9122216),
 new google.maps.LatLng(49.2044068,-122.9122216),
 new google.maps.LatLng(49.2005375,-122.9177303),
 new google.maps.LatLng(49.2005375,-122.9177303),
 new google.maps.LatLng(49.2005375,-122.9177303),
 new google.maps.LatLng(49.2044068,-122.9122216),
 new google.maps.LatLng(49.20176432,-122.91093161),
 new google.maps.LatLng(49.20182415,-122.91087463),
 new google.maps.LatLng(49.20182199,-122.9108727),
 new google.maps.LatLng(49.20182815,-122.91086861),
 new google.maps.LatLng(49.20180197,-122.91091456),
 new google.maps.LatLng(49.20182378,-122.91087454),
 new google.maps.LatLng(49.20182318,-122.91087434),
 new google.maps.LatLng(49.2018017,-122.91091647),
 new google.maps.LatLng(49.20182285,-122.91087346),
 new google.maps.LatLng(49.20181527,-122.91088917),
 new google.maps.LatLng(49.20182816,-122.9108682),
 new google.maps.LatLng(49.20182815,-122.91086877),
 new google.maps.LatLng(49.20182815,-122.91086799),
 new google.maps.LatLng(49.201828,-122.91086762),
 new google.maps.LatLng(49.20182791,-122.91086753),
 new google.maps.LatLng(49.2018719,-122.9110207),
 new google.maps.LatLng(49.2829871,-123.0483724),
 new google.maps.LatLng(49.2829292,-123.0484088),
 new google.maps.LatLng(49.2829837,-123.0483806),
 new google.maps.LatLng(49.2829731,-123.0483733),
 new google.maps.LatLng(49.2829731,-123.0483733), ]; 

                var flightPath = new google.maps.Polyline({
                    path: flightPlanCoordinates,
                    geodesic: true,
                    strokeColor: '#E57373', //taken from material design by google
                    strokeOpacity: 1,
                    strokeWeight: 2
                });

                function AddPolyFill0(map){
                    flightPath.setMap(map);
                }function initialize() {
                        var mapOptions = {
                          center: { lat: 48.3822, lng: -89.2461},
                          zoom: 4
                        };
                        var map = new google.maps.Map(document.getElementById('map-canvas'),
                            mapOptions);
         
                        //generated by the server every set period of time!
                        	AddPolyFill0(map);
                      }
                      google.maps.event.addDomListener(window, 'load', initialize);