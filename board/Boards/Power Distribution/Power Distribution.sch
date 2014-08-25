<?xml version="1.0" encoding="utf-8"?>
<!DOCTYPE eagle SYSTEM "eagle.dtd">
<eagle version="6.5.0">
<drawing>
<settings>
<setting alwaysvectorfont="no"/>
<setting verticaltext="up"/>
</settings>
<grid distance="0.1" unitdist="inch" unit="inch" style="lines" multiple="1" display="no" altdistance="0.01" altunitdist="inch" altunit="inch"/>
<layers>
<layer number="1" name="Top" color="4" fill="1" visible="no" active="no"/>
<layer number="2" name="Route2" color="1" fill="3" visible="no" active="no"/>
<layer number="3" name="Route3" color="4" fill="3" visible="no" active="no"/>
<layer number="4" name="Route4" color="1" fill="4" visible="no" active="no"/>
<layer number="5" name="Route5" color="4" fill="4" visible="no" active="no"/>
<layer number="6" name="Route6" color="1" fill="8" visible="no" active="no"/>
<layer number="7" name="Route7" color="4" fill="8" visible="no" active="no"/>
<layer number="8" name="Route8" color="1" fill="2" visible="no" active="no"/>
<layer number="9" name="Route9" color="4" fill="2" visible="no" active="no"/>
<layer number="10" name="Route10" color="1" fill="7" visible="no" active="no"/>
<layer number="11" name="Route11" color="4" fill="7" visible="no" active="no"/>
<layer number="12" name="Route12" color="1" fill="5" visible="no" active="no"/>
<layer number="13" name="Route13" color="4" fill="5" visible="no" active="no"/>
<layer number="14" name="Route14" color="1" fill="6" visible="no" active="no"/>
<layer number="15" name="Route15" color="4" fill="6" visible="no" active="no"/>
<layer number="16" name="Bottom" color="1" fill="1" visible="no" active="no"/>
<layer number="17" name="Pads" color="2" fill="1" visible="no" active="no"/>
<layer number="18" name="Vias" color="2" fill="1" visible="no" active="no"/>
<layer number="19" name="Unrouted" color="6" fill="1" visible="no" active="no"/>
<layer number="20" name="Dimension" color="15" fill="1" visible="no" active="no"/>
<layer number="21" name="tPlace" color="7" fill="1" visible="no" active="no"/>
<layer number="22" name="bPlace" color="7" fill="1" visible="no" active="no"/>
<layer number="23" name="tOrigins" color="15" fill="1" visible="no" active="no"/>
<layer number="24" name="bOrigins" color="15" fill="1" visible="no" active="no"/>
<layer number="25" name="tNames" color="7" fill="1" visible="no" active="no"/>
<layer number="26" name="bNames" color="7" fill="1" visible="no" active="no"/>
<layer number="27" name="tValues" color="7" fill="1" visible="no" active="no"/>
<layer number="28" name="bValues" color="7" fill="1" visible="no" active="no"/>
<layer number="29" name="tStop" color="7" fill="3" visible="no" active="no"/>
<layer number="30" name="bStop" color="7" fill="6" visible="no" active="no"/>
<layer number="31" name="tCream" color="7" fill="4" visible="no" active="no"/>
<layer number="32" name="bCream" color="7" fill="5" visible="no" active="no"/>
<layer number="33" name="tFinish" color="6" fill="3" visible="no" active="no"/>
<layer number="34" name="bFinish" color="6" fill="6" visible="no" active="no"/>
<layer number="35" name="tGlue" color="7" fill="4" visible="no" active="no"/>
<layer number="36" name="bGlue" color="7" fill="5" visible="no" active="no"/>
<layer number="37" name="tTest" color="7" fill="1" visible="no" active="no"/>
<layer number="38" name="bTest" color="7" fill="1" visible="no" active="no"/>
<layer number="39" name="tKeepout" color="4" fill="11" visible="no" active="no"/>
<layer number="40" name="bKeepout" color="1" fill="11" visible="no" active="no"/>
<layer number="41" name="tRestrict" color="4" fill="10" visible="no" active="no"/>
<layer number="42" name="bRestrict" color="1" fill="10" visible="no" active="no"/>
<layer number="43" name="vRestrict" color="2" fill="10" visible="no" active="no"/>
<layer number="44" name="Drills" color="7" fill="1" visible="no" active="no"/>
<layer number="45" name="Holes" color="7" fill="1" visible="no" active="no"/>
<layer number="46" name="Milling" color="3" fill="1" visible="no" active="no"/>
<layer number="47" name="Measures" color="7" fill="1" visible="no" active="no"/>
<layer number="48" name="Document" color="7" fill="1" visible="no" active="no"/>
<layer number="49" name="Reference" color="7" fill="1" visible="no" active="no"/>
<layer number="51" name="tDocu" color="7" fill="1" visible="no" active="no"/>
<layer number="52" name="bDocu" color="7" fill="1" visible="no" active="no"/>
<layer number="91" name="Nets" color="2" fill="1" visible="yes" active="yes"/>
<layer number="92" name="Busses" color="1" fill="1" visible="yes" active="yes"/>
<layer number="93" name="Pins" color="2" fill="1" visible="no" active="yes"/>
<layer number="94" name="Symbols" color="4" fill="1" visible="yes" active="yes"/>
<layer number="95" name="Names" color="7" fill="1" visible="yes" active="yes"/>
<layer number="96" name="Values" color="7" fill="1" visible="yes" active="yes"/>
<layer number="97" name="Info" color="7" fill="1" visible="yes" active="yes"/>
<layer number="98" name="Guide" color="6" fill="1" visible="yes" active="yes"/>
</layers>
<schematic xreflabel="%F%N/%S.%C%R" xrefpart="/%S.%C%R">
<libraries>
<library name="Molex">
<packages>
<package name="2X2MOLEX">
<pad name="POWERL" x="-2.1" y="2.1" drill="1.8"/>
<pad name="POWERR" x="2.1" y="2.1" drill="1.8"/>
<pad name="GNDR" x="2.1" y="-2.1" drill="1.8"/>
<pad name="GNDL" x="-2.1" y="-2.1" drill="1.8"/>
<hole x="0" y="-10.05" drill="3"/>
<wire x1="-6" y1="-16" x2="6" y2="-16" width="0.127" layer="21"/>
<wire x1="6" y1="-16" x2="6" y2="-6" width="0.127" layer="21"/>
<wire x1="6" y1="-6" x2="6" y2="4" width="0.127" layer="21"/>
<wire x1="6" y1="4" x2="-6" y2="4" width="0.127" layer="21"/>
<wire x1="-6" y1="4" x2="-6" y2="-6" width="0.127" layer="21"/>
<wire x1="-6" y1="-6" x2="-6" y2="-16" width="0.127" layer="21"/>
<wire x1="-6" y1="-6" x2="6" y2="-6" width="0.127" layer="21"/>
</package>
</packages>
<symbols>
<symbol name="2X2MOLEX">
<wire x1="-7.62" y1="10.16" x2="7.62" y2="10.16" width="0.254" layer="94"/>
<wire x1="7.62" y1="10.16" x2="7.62" y2="-5.08" width="0.254" layer="94"/>
<wire x1="7.62" y1="-5.08" x2="-7.62" y2="-5.08" width="0.254" layer="94"/>
<wire x1="-7.62" y1="-5.08" x2="-7.62" y2="10.16" width="0.254" layer="94"/>
<pin name="GND" x="2.54" y="12.7" length="middle" rot="R270"/>
<pin name="POWER" x="-2.54" y="12.7" length="middle" rot="R270"/>
</symbol>
</symbols>
<devicesets>
<deviceset name="2X2-MOLEX">
<gates>
<gate name="G$1" symbol="2X2MOLEX" x="0" y="-2.54"/>
</gates>
<devices>
<device name="" package="2X2MOLEX">
<connects>
<connect gate="G$1" pin="GND" pad="GNDL GNDR"/>
<connect gate="G$1" pin="POWER" pad="POWERL POWERR"/>
</connects>
<technologies>
<technology name=""/>
</technologies>
</device>
</devices>
</deviceset>
</devicesets>
</library>
<library name="supply2">
<description>&lt;b&gt;Supply Symbols&lt;/b&gt;&lt;p&gt;
GND, VCC, 0V, +5V, -5V, etc.&lt;p&gt;
Please keep in mind, that these devices are necessary for the
automatic wiring of the supply signals.&lt;p&gt;
The pin name defined in the symbol is identical to the net which is to be wired automatically.&lt;p&gt;
In this library the device names are the same as the pin names of the symbols, therefore the correct signal names appear next to the supply symbols in the schematic.&lt;p&gt;
&lt;author&gt;Created by librarian@cadsoft.de&lt;/author&gt;</description>
<packages>
</packages>
<symbols>
<symbol name="GND">
<wire x1="-1.27" y1="0" x2="1.27" y2="0" width="0.254" layer="94"/>
<wire x1="1.27" y1="0" x2="0" y2="-1.27" width="0.254" layer="94"/>
<wire x1="0" y1="-1.27" x2="-1.27" y2="0" width="0.254" layer="94"/>
<text x="-1.905" y="-3.175" size="1.778" layer="96">&gt;VALUE</text>
<pin name="GND" x="0" y="2.54" visible="off" length="short" direction="sup" rot="R270"/>
</symbol>
</symbols>
<devicesets>
<deviceset name="GND" prefix="SUPPLY">
<description>&lt;b&gt;SUPPLY SYMBOL&lt;/b&gt;</description>
<gates>
<gate name="GND" symbol="GND" x="0" y="0"/>
</gates>
<devices>
<device name="">
<technologies>
<technology name=""/>
</technologies>
</device>
</devices>
</deviceset>
</devicesets>
</library>
<library name="Diodes">
<packages>
<package name="DO-214AB">
<smd name="IN" x="-3.302" y="0" dx="5.08" dy="1.778" layer="1" rot="R90"/>
<smd name="OUT" x="3.302" y="0" dx="5.08" dy="1.778" layer="1" rot="R90"/>
<wire x1="-5.08" y1="3.81" x2="5.08" y2="3.81" width="0.127" layer="21"/>
<wire x1="5.08" y1="3.81" x2="5.08" y2="-3.81" width="0.127" layer="21"/>
<wire x1="5.08" y1="-3.81" x2="-5.08" y2="-3.81" width="0.127" layer="21"/>
<wire x1="-5.08" y1="-3.81" x2="-5.08" y2="3.81" width="0.127" layer="21"/>
<wire x1="1.27" y1="2.54" x2="1.27" y2="-2.54" width="0.6096" layer="21"/>
</package>
</packages>
<symbols>
<symbol name="GENERICDIODE">
<wire x1="-5.08" y1="0" x2="0" y2="7.62" width="0.254" layer="94"/>
<wire x1="0" y1="7.62" x2="5.08" y2="0" width="0.254" layer="94"/>
<wire x1="5.08" y1="0" x2="-5.08" y2="0" width="0.254" layer="94"/>
<wire x1="-5.08" y1="7.62" x2="5.08" y2="7.62" width="0.254" layer="94"/>
<pin name="OUT" x="0" y="10.16" length="middle" rot="R270"/>
<pin name="IN" x="0" y="-2.54" length="middle" rot="R90"/>
<wire x1="-5.08" y1="7.62" x2="-5.08" y2="5.08" width="0.254" layer="94"/>
<wire x1="5.08" y1="7.62" x2="5.08" y2="10.16" width="0.254" layer="94"/>
</symbol>
</symbols>
<devicesets>
<deviceset name="DO-214AB">
<gates>
<gate name="G$1" symbol="GENERICDIODE" x="0" y="0"/>
</gates>
<devices>
<device name="" package="DO-214AB">
<connects>
<connect gate="G$1" pin="IN" pad="IN"/>
<connect gate="G$1" pin="OUT" pad="OUT"/>
</connects>
<technologies>
<technology name=""/>
</technologies>
</device>
</devices>
</deviceset>
</devicesets>
</library>
<library name="testpad">
<description>&lt;b&gt;Test Pins/Pads&lt;/b&gt;&lt;p&gt;
Cream on SMD OFF.&lt;br&gt;
new: Attribute TP_SIGNAL_NAME&lt;br&gt;
&lt;author&gt;Created by librarian@cadsoft.de&lt;/author&gt;</description>
<packages>
<package name="P5-25-13">
<description>&lt;b&gt;TEST PAD&lt;/b&gt;</description>
<circle x="0" y="0" radius="0.762" width="0.1524" layer="51"/>
<circle x="2.54" y="0" radius="0.762" width="0.1524" layer="51"/>
<circle x="5.08" y="0" radius="0.762" width="0.1524" layer="51"/>
<circle x="-2.54" y="0" radius="0.762" width="0.1524" layer="51"/>
<circle x="-5.08" y="0" radius="0.762" width="0.1524" layer="51"/>
<pad name="TP-1" x="-5.08" y="0" drill="1.3208" diameter="1.905" shape="long" rot="R90"/>
<pad name="TP-2" x="-2.54" y="0" drill="1.3208" diameter="1.905" shape="long" rot="R90"/>
<pad name="TP-3" x="0" y="0" drill="1.3208" diameter="1.905" shape="long" rot="R90"/>
<pad name="TP-4" x="2.54" y="0" drill="1.3208" diameter="1.905" shape="long" rot="R90"/>
<pad name="TP-5" x="5.08" y="0" drill="1.3208" diameter="1.905" shape="long" rot="R90"/>
<text x="-5.969" y="2.159" size="1.27" layer="25" ratio="10">&gt;NAME</text>
<text x="0" y="2.159" size="1.27" layer="27" ratio="10">&gt;VALUE</text>
<text x="-5.715" y="-3.81" size="1" layer="37">&gt;TP_SIGNAL_NAME</text>
<rectangle x1="-0.3302" y1="-0.3302" x2="0.3302" y2="0.3302" layer="51"/>
<rectangle x1="2.2098" y1="-0.3302" x2="2.8702" y2="0.3302" layer="51"/>
<rectangle x1="4.7498" y1="-0.3302" x2="5.4102" y2="0.3302" layer="51"/>
<rectangle x1="-2.8702" y1="-0.3302" x2="-2.2098" y2="0.3302" layer="51"/>
<rectangle x1="-5.4102" y1="-0.3302" x2="-4.7498" y2="0.3302" layer="51"/>
</package>
<package name="P5-25-17">
<description>&lt;b&gt;TEST PAD&lt;/b&gt;</description>
<circle x="0" y="0" radius="0.8128" width="0.1524" layer="51"/>
<circle x="-5.08" y="0" radius="0.8128" width="0.1524" layer="51"/>
<circle x="-2.54" y="0" radius="0.8128" width="0.1524" layer="51"/>
<circle x="2.54" y="0" radius="0.8128" width="0.1524" layer="51"/>
<circle x="5.08" y="0" radius="0.8128" width="0.1524" layer="51"/>
<pad name="TP-1" x="-5.08" y="0" drill="1.7018" diameter="1.9304" shape="long" rot="R90"/>
<pad name="TP-2" x="-2.54" y="0" drill="1.7018" diameter="1.9304" shape="long" rot="R90"/>
<pad name="TP-3" x="0" y="0" drill="1.7018" diameter="1.9304" shape="long" rot="R90"/>
<pad name="TP-4" x="2.54" y="0" drill="1.7018" diameter="1.9304" shape="long" rot="R90"/>
<pad name="TP-5" x="5.08" y="0" drill="1.7018" diameter="1.9304" shape="long" rot="R90"/>
<text x="-6.096" y="2.54" size="1.27" layer="25" ratio="10">&gt;NAME</text>
<text x="0" y="2.54" size="1.27" layer="27" ratio="10">&gt;VALUE</text>
<text x="-5.715" y="-3.81" size="1" layer="37">&gt;TP_SIGNAL_NAME</text>
<rectangle x1="-0.3302" y1="-0.3302" x2="0.3302" y2="0.3302" layer="51"/>
<rectangle x1="-5.4102" y1="-0.3302" x2="-4.7498" y2="0.3302" layer="51"/>
<rectangle x1="-2.8702" y1="-0.3302" x2="-2.2098" y2="0.3302" layer="51"/>
<rectangle x1="2.2098" y1="-0.3302" x2="2.8702" y2="0.3302" layer="51"/>
<rectangle x1="4.7498" y1="-0.3302" x2="5.4102" y2="0.3302" layer="51"/>
</package>
</packages>
<symbols>
<symbol name="TP">
<wire x1="-0.762" y1="-0.762" x2="0" y2="0" width="0.254" layer="94"/>
<wire x1="0" y1="0" x2="0.762" y2="-0.762" width="0.254" layer="94"/>
<wire x1="0.762" y1="-0.762" x2="0" y2="-1.524" width="0.254" layer="94"/>
<wire x1="0" y1="-1.524" x2="-0.762" y2="-0.762" width="0.254" layer="94"/>
<text x="-1.27" y="1.27" size="1.778" layer="95">&gt;NAME</text>
<text x="1.27" y="-1.27" size="1.778" layer="97">&gt;TP_SIGNAL_NAME</text>
<pin name="TP" x="0" y="-2.54" visible="off" length="short" direction="in" rot="R90"/>
</symbol>
</symbols>
<devicesets>
<deviceset name="TP5" prefix="TP">
<description>&lt;b&gt;Test pad&lt;/b&gt;</description>
<gates>
<gate name="A" symbol="TP" x="-10.16" y="0" addlevel="always"/>
<gate name="B" symbol="TP" x="-5.08" y="0" addlevel="always"/>
<gate name="C" symbol="TP" x="0" y="0" addlevel="always"/>
<gate name="D" symbol="TP" x="5.08" y="0" addlevel="always"/>
<gate name="E" symbol="TP" x="10.16" y="0" addlevel="always"/>
</gates>
<devices>
<device name="P5-25-13" package="P5-25-13">
<connects>
<connect gate="A" pin="TP" pad="TP-1"/>
<connect gate="B" pin="TP" pad="TP-2"/>
<connect gate="C" pin="TP" pad="TP-3"/>
<connect gate="D" pin="TP" pad="TP-4"/>
<connect gate="E" pin="TP" pad="TP-5"/>
</connects>
<technologies>
<technology name="">
<attribute name="TP_SIGNAL_NAME" value="" constant="no"/>
</technology>
</technologies>
</device>
<device name="P5-25-17" package="P5-25-17">
<connects>
<connect gate="A" pin="TP" pad="TP-1"/>
<connect gate="B" pin="TP" pad="TP-2"/>
<connect gate="C" pin="TP" pad="TP-3"/>
<connect gate="D" pin="TP" pad="TP-4"/>
<connect gate="E" pin="TP" pad="TP-5"/>
</connects>
<technologies>
<technology name="">
<attribute name="TP_SIGNAL_NAME" value="" constant="no"/>
</technology>
</technologies>
</device>
</devices>
</deviceset>
</devicesets>
</library>
</libraries>
<attributes>
</attributes>
<variantdefs>
</variantdefs>
<classes>
<class number="0" name="default" width="0" drill="0">
</class>
</classes>
<parts>
<part name="U$1" library="Molex" deviceset="2X2-MOLEX" device=""/>
<part name="U$2" library="Molex" deviceset="2X2-MOLEX" device=""/>
<part name="U$3" library="Molex" deviceset="2X2-MOLEX" device=""/>
<part name="U$4" library="Molex" deviceset="2X2-MOLEX" device=""/>
<part name="U$5" library="Molex" deviceset="2X2-MOLEX" device=""/>
<part name="SUPPLY1" library="supply2" deviceset="GND" device=""/>
<part name="U$6" library="Diodes" deviceset="DO-214AB" device=""/>
<part name="U$7" library="Diodes" deviceset="DO-214AB" device=""/>
<part name="U$8" library="Diodes" deviceset="DO-214AB" device=""/>
<part name="U$9" library="Diodes" deviceset="DO-214AB" device=""/>
<part name="U$10" library="Diodes" deviceset="DO-214AB" device=""/>
<part name="TP1" library="testpad" deviceset="TP5" device="P5-25-17"/>
<part name="TP2" library="testpad" deviceset="TP5" device="P5-25-17"/>
<part name="TP3" library="testpad" deviceset="TP5" device="P5-25-17"/>
<part name="TP4" library="testpad" deviceset="TP5" device="P5-25-17"/>
</parts>
<sheets>
<sheet>
<plain>
</plain>
<instances>
<instance part="U$1" gate="G$1" x="10.16" y="78.74" rot="R270"/>
<instance part="U$2" gate="G$1" x="10.16" y="58.42" rot="R270"/>
<instance part="U$3" gate="G$1" x="10.16" y="35.56" rot="R270"/>
<instance part="U$4" gate="G$1" x="10.16" y="12.7" rot="R270"/>
<instance part="U$5" gate="G$1" x="10.16" y="99.06" rot="R270"/>
<instance part="SUPPLY1" gate="GND" x="25.4" y="2.54"/>
<instance part="U$6" gate="G$1" x="35.56" y="101.6" rot="R270"/>
<instance part="U$7" gate="G$1" x="35.56" y="81.28" rot="R270"/>
<instance part="U$8" gate="G$1" x="35.56" y="60.96" rot="R270"/>
<instance part="U$9" gate="G$1" x="35.56" y="38.1" rot="R270"/>
<instance part="U$10" gate="G$1" x="35.56" y="15.24" rot="R270"/>
<instance part="TP1" gate="A" x="71.12" y="104.14"/>
<instance part="TP1" gate="B" x="76.2" y="104.14"/>
<instance part="TP1" gate="C" x="81.28" y="104.14"/>
<instance part="TP1" gate="D" x="86.36" y="104.14"/>
<instance part="TP1" gate="E" x="91.44" y="104.14"/>
<instance part="TP2" gate="A" x="71.12" y="91.44"/>
<instance part="TP2" gate="B" x="76.2" y="91.44"/>
<instance part="TP2" gate="C" x="81.28" y="91.44"/>
<instance part="TP2" gate="D" x="86.36" y="91.44"/>
<instance part="TP2" gate="E" x="91.44" y="91.44"/>
<instance part="TP3" gate="A" x="35.56" y="0"/>
<instance part="TP3" gate="B" x="40.64" y="0"/>
<instance part="TP3" gate="C" x="45.72" y="0"/>
<instance part="TP3" gate="D" x="50.8" y="0"/>
<instance part="TP3" gate="E" x="55.88" y="0"/>
<instance part="TP4" gate="A" x="35.56" y="-10.16"/>
<instance part="TP4" gate="B" x="40.64" y="-10.16"/>
<instance part="TP4" gate="C" x="45.72" y="-10.16"/>
<instance part="TP4" gate="D" x="50.8" y="-10.16"/>
<instance part="TP4" gate="E" x="55.88" y="-10.16"/>
</instances>
<busses>
</busses>
<nets>
<net name="GND" class="0">
<segment>
<pinref part="U$3" gate="G$1" pin="GND"/>
<pinref part="SUPPLY1" gate="GND" pin="GND"/>
<wire x1="22.86" y1="33.02" x2="25.4" y2="33.02" width="0.1524" layer="91"/>
<wire x1="25.4" y1="33.02" x2="25.4" y2="10.16" width="0.1524" layer="91"/>
<pinref part="U$4" gate="G$1" pin="GND"/>
<wire x1="25.4" y1="10.16" x2="25.4" y2="5.08" width="0.1524" layer="91"/>
<wire x1="22.86" y1="10.16" x2="25.4" y2="10.16" width="0.1524" layer="91"/>
<wire x1="25.4" y1="96.52" x2="25.4" y2="76.2" width="0.1524" layer="91"/>
<pinref part="U$2" gate="G$1" pin="GND"/>
<wire x1="25.4" y1="76.2" x2="25.4" y2="55.88" width="0.1524" layer="91"/>
<wire x1="25.4" y1="55.88" x2="25.4" y2="33.02" width="0.1524" layer="91"/>
<wire x1="22.86" y1="55.88" x2="25.4" y2="55.88" width="0.1524" layer="91"/>
<pinref part="U$1" gate="G$1" pin="GND"/>
<wire x1="22.86" y1="76.2" x2="25.4" y2="76.2" width="0.1524" layer="91"/>
<pinref part="U$5" gate="G$1" pin="GND"/>
<wire x1="22.86" y1="96.52" x2="25.4" y2="96.52" width="0.1524" layer="91"/>
<junction x="25.4" y="96.52"/>
<junction x="25.4" y="76.2"/>
<junction x="25.4" y="55.88"/>
<junction x="25.4" y="33.02"/>
<junction x="25.4" y="10.16"/>
<wire x1="25.4" y1="5.08" x2="30.48" y2="5.08" width="0.1524" layer="91"/>
<wire x1="30.48" y1="5.08" x2="30.48" y2="-2.54" width="0.1524" layer="91"/>
<pinref part="TP3" gate="A" pin="TP"/>
<wire x1="30.48" y1="-2.54" x2="35.56" y2="-2.54" width="0.1524" layer="91"/>
<pinref part="TP3" gate="B" pin="TP"/>
<wire x1="35.56" y1="-2.54" x2="40.64" y2="-2.54" width="0.1524" layer="91"/>
<pinref part="TP3" gate="C" pin="TP"/>
<wire x1="40.64" y1="-2.54" x2="45.72" y2="-2.54" width="0.1524" layer="91"/>
<pinref part="TP3" gate="D" pin="TP"/>
<wire x1="45.72" y1="-2.54" x2="50.8" y2="-2.54" width="0.1524" layer="91"/>
<pinref part="TP3" gate="E" pin="TP"/>
<wire x1="50.8" y1="-2.54" x2="55.88" y2="-2.54" width="0.1524" layer="91"/>
<wire x1="30.48" y1="-2.54" x2="30.48" y2="-12.7" width="0.1524" layer="91"/>
<pinref part="TP4" gate="A" pin="TP"/>
<wire x1="30.48" y1="-12.7" x2="35.56" y2="-12.7" width="0.1524" layer="91"/>
<pinref part="TP4" gate="B" pin="TP"/>
<wire x1="35.56" y1="-12.7" x2="40.64" y2="-12.7" width="0.1524" layer="91"/>
<pinref part="TP4" gate="C" pin="TP"/>
<wire x1="40.64" y1="-12.7" x2="45.72" y2="-12.7" width="0.1524" layer="91"/>
<pinref part="TP4" gate="D" pin="TP"/>
<wire x1="45.72" y1="-12.7" x2="50.8" y2="-12.7" width="0.1524" layer="91"/>
<pinref part="TP4" gate="E" pin="TP"/>
<wire x1="50.8" y1="-12.7" x2="55.88" y2="-12.7" width="0.1524" layer="91"/>
</segment>
</net>
<net name="N$1" class="0">
<segment>
<pinref part="U$5" gate="G$1" pin="POWER"/>
<pinref part="U$6" gate="G$1" pin="IN"/>
<wire x1="22.86" y1="101.6" x2="33.02" y2="101.6" width="0.1524" layer="91"/>
</segment>
</net>
<net name="N$2" class="0">
<segment>
<pinref part="U$1" gate="G$1" pin="POWER"/>
<pinref part="U$7" gate="G$1" pin="IN"/>
<wire x1="22.86" y1="81.28" x2="33.02" y2="81.28" width="0.1524" layer="91"/>
</segment>
</net>
<net name="N$3" class="0">
<segment>
<pinref part="U$2" gate="G$1" pin="POWER"/>
<pinref part="U$8" gate="G$1" pin="IN"/>
<wire x1="22.86" y1="60.96" x2="33.02" y2="60.96" width="0.1524" layer="91"/>
</segment>
</net>
<net name="N$4" class="0">
<segment>
<pinref part="U$3" gate="G$1" pin="POWER"/>
<pinref part="U$9" gate="G$1" pin="IN"/>
<wire x1="22.86" y1="38.1" x2="33.02" y2="38.1" width="0.1524" layer="91"/>
</segment>
</net>
<net name="N$5" class="0">
<segment>
<pinref part="U$4" gate="G$1" pin="POWER"/>
<pinref part="U$10" gate="G$1" pin="IN"/>
<wire x1="22.86" y1="15.24" x2="33.02" y2="15.24" width="0.1524" layer="91"/>
</segment>
</net>
<net name="N$6" class="0">
<segment>
<pinref part="U$6" gate="G$1" pin="OUT"/>
<wire x1="45.72" y1="101.6" x2="55.88" y2="101.6" width="0.1524" layer="91"/>
<wire x1="55.88" y1="101.6" x2="55.88" y2="86.36" width="0.1524" layer="91"/>
<pinref part="U$10" gate="G$1" pin="OUT"/>
<wire x1="55.88" y1="86.36" x2="55.88" y2="81.28" width="0.1524" layer="91"/>
<wire x1="55.88" y1="81.28" x2="55.88" y2="60.96" width="0.1524" layer="91"/>
<wire x1="55.88" y1="60.96" x2="55.88" y2="38.1" width="0.1524" layer="91"/>
<wire x1="55.88" y1="38.1" x2="55.88" y2="15.24" width="0.1524" layer="91"/>
<wire x1="55.88" y1="15.24" x2="45.72" y2="15.24" width="0.1524" layer="91"/>
<pinref part="U$9" gate="G$1" pin="OUT"/>
<wire x1="45.72" y1="38.1" x2="55.88" y2="38.1" width="0.1524" layer="91"/>
<pinref part="U$8" gate="G$1" pin="OUT"/>
<wire x1="45.72" y1="60.96" x2="55.88" y2="60.96" width="0.1524" layer="91"/>
<pinref part="U$7" gate="G$1" pin="OUT"/>
<wire x1="45.72" y1="81.28" x2="55.88" y2="81.28" width="0.1524" layer="91"/>
<pinref part="TP1" gate="A" pin="TP"/>
<wire x1="55.88" y1="101.6" x2="71.12" y2="101.6" width="0.1524" layer="91"/>
<pinref part="TP1" gate="B" pin="TP"/>
<wire x1="71.12" y1="101.6" x2="76.2" y2="101.6" width="0.1524" layer="91"/>
<pinref part="TP1" gate="C" pin="TP"/>
<wire x1="76.2" y1="101.6" x2="81.28" y2="101.6" width="0.1524" layer="91"/>
<pinref part="TP1" gate="D" pin="TP"/>
<wire x1="81.28" y1="101.6" x2="86.36" y2="101.6" width="0.1524" layer="91"/>
<pinref part="TP1" gate="E" pin="TP"/>
<wire x1="86.36" y1="101.6" x2="91.44" y2="101.6" width="0.1524" layer="91"/>
<pinref part="TP2" gate="A" pin="TP"/>
<wire x1="55.88" y1="86.36" x2="71.12" y2="86.36" width="0.1524" layer="91"/>
<wire x1="71.12" y1="86.36" x2="71.12" y2="88.9" width="0.1524" layer="91"/>
<pinref part="TP2" gate="B" pin="TP"/>
<wire x1="71.12" y1="88.9" x2="76.2" y2="88.9" width="0.1524" layer="91"/>
<pinref part="TP2" gate="C" pin="TP"/>
<wire x1="76.2" y1="88.9" x2="81.28" y2="88.9" width="0.1524" layer="91"/>
<pinref part="TP2" gate="D" pin="TP"/>
<wire x1="81.28" y1="88.9" x2="86.36" y2="88.9" width="0.1524" layer="91"/>
<pinref part="TP2" gate="E" pin="TP"/>
<wire x1="86.36" y1="88.9" x2="91.44" y2="88.9" width="0.1524" layer="91"/>
</segment>
</net>
</nets>
</sheet>
</sheets>
</schematic>
</drawing>
</eagle>
