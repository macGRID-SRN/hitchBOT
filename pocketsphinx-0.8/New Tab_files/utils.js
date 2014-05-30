/*
 *  Onoko JavaScript Utilities
 */
function findObject(target, key, value) {
	for(var i = 0; i < target.length; i++){
		var row = target[i];
		if(row[key] == value) {
			return row;
		}
	}
}

function findObjectSmart(target, keyvaluepairs) {
	for(var i = 0; i < target.length; i++){
		var row = target[i];
		var found = true;
		for (var key in keyvaluepairs) {
			var value = keyvaluepairs[key];
			if(row[key] != value) {
				found = false;
				break;
			}
		}
		if (found) {
			return row;
		}
	}
}

function dumpObject(object, depth, max) {
	depth = depth || 0;
	max = max || 2;
	
	if (depth > max) {
		return false;
	}
	
	var indent = "";
	for (var i = 0; i < depth; i++) {
		indent += "  ";
	}
	
	var output = "";  
	for (var key in object) {
		output += "\n" + indent + key + ": ";
		switch (typeof object[key]){
			case "object": output += dumpObject(object[key], depth + 1, max); break;
			case "function": output += "function"; break;
			default: output += object[key]; break;
		}
	}
	return output;
}

function cloneObject(obj) {
	if(obj == null || typeof(obj) != 'object') {
		return obj;
	}
	var temp = new obj.constructor(); // changed (twice)
	for(var key in obj) {
		temp[key] = cloneObject(obj[key]);
	}
	return temp;
}

function replaceAll(txt, replace, with_this) {
	return txt.replace(new RegExp(replace, 'g'),with_this);
}

function blank(item) {
	return item == null || item == '';
}
function equalNotNull(item, new_item) {
	return item != null && item != new_item;
}

function equalNotNullArray(item, new_item, id_string) {
	if(isArray(id_string)) {
		for (var i in id_string) {
			if (!equalNotNullArray(item,new_item,id_string[i])) {
				return false;
			}
		}
		return true;
	} else if (id_string) {
		return item[id_string] && new_item[id_string] && item[id_string] == new_item[id_string];
	}
}

function countObject(obj) {
	var count = 0;
	if (obj) {
		for (var i in obj) {
			if (obj.hasOwnProperty(i)) {
				++count;
			}
		}
	}
	return count;
}

function inArray(value, array, insensitive, first) {
	var duplicate = array.slice(0);
	if(insensitive) { for(var i in duplicate){ duplicate[i] = duplicate[i].toLowerCase(); } }
	for(var i in duplicate) {
		if(first) {
			if(duplicate[i].indexOf(value) == 0) {
				return i;
			}
		} else {
			if(duplicate[i] == value) {
				return i;
			}
		}
	}
	return false;
}

function magnitude(x) {
	return (x<0?-x:x);
}

function isNumber(n) {
	return !isNaN(parseFloat(n));
}

function isString(s) {
	return typeof(s)=='string';
}

function isArray(a) {
	return typeof(a)=='object' && a.length != undefined;
}

function isObject(o) {
	return typeof(o)=='object' && o.length == undefined;
}

function isEmail(email) { 
	var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
	return re.test(email);
}

function isImageFile(filename) {
	var exts = ['.png','.jpg','.jpeg','.gif','.bmp'];
	filename = filename.toLowerCase();
	for (var i in exts) {
		var ext = exts[i];
		if (filename.indexOf(ext) == filename.length - ext.length) {
			return true;
		}
	}
	return false;
}

function capitalize(s) {
	return s.charAt(0).toUpperCase() + s.slice(1);
}

function timestamp(d) {
	return Math.round((d ? d : new Date()).getTime()/1000);
}

function daystart(d) {
	d = d || new Date();
	return new Date(d.getFullYear(),d.getMonth(),d.getDate(),0,0,0,0);
}

function TimeZoneInfo() {
	var now = new Date();
	var year = now.getFullYear();
	var june = new Date(Date.UTC(year, 6, 30, 0, 0, 0, 0));
	var dec = new Date(Date.UTC(year, 12, 30, 0, 0, 0, 0));
	//Use < ecause getTimezoneOffset is returns negative, eg. UTC+1 = -60
	//Could equally be called isNorthanHemisphere
	this.forwardInSummer = june.getTimezoneOffset() < dec.getTimezoneOffset();
	this.currentOffset = now.getTimezoneOffset();
	if (this.forwardInSummer) {
		this.isDaylightSavings = june.getTimezoneOffset() == this.currentOffset;
		this.normalOffset = dec.getTimezoneOffset();
		this.DSTOffset = june.getTimezoneOffset();
	} else {
		this.isDaylightSavings = dec.getTimezoneOffset() == this.currentOffset;
		this.normalOffset = june.getTimezoneOffset();
		this.DSTOffset = dec.getTimezoneOffset();
	}
}

//Get default timezone from time zone info
function getDefaultTimezone() {
	//PHP map: http://codingforums.com/showthread.php?t=139888
}

//Get default locale from combination of IP address (and timezone???)
function getDefaultLocale() {
	//PHP http://php.net/manual/en/function.setlocale.php
}

function trim(s) {
	return s.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
}

function random(max) {
	return Math.floor(Math.random()*(max+1));
}

function randomarray(arr) {
	return arr[random(arr.length-1)];
}

function popRandomArray(arr) {
	return arr.splice(random(arr.length-1), 1)[0];
}

function generateRandomId(len, onlyupper) {
	var NUMERIC = '0123456789';
	var ALPHA_UPPER = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
	ALPHA_NUMERIC = NUMERIC + ALPHA_UPPER;
	if (!onlyupper) {
		var ALPHA_LOWER = 'abcdefghijklmnopqrstuvwxyz';
		ALPHA_NUMERIC += ALPHA_LOWER;
	}
	var result = '';
	for (var i=0; i<len; i++) {
		result += randomarray(ALPHA_NUMERIC);
	}
	return result;
}

function generateCalDAVId() {
	return generateRandomId(8,true)+'-'+generateRandomId(4,true)+'-'+generateRandomId(4,true)+'-'+generateRandomId(4,true)+'-'+generateRandomId(12,true);
}

function randomColor() {
	return ('#0' + Math.round(0xffffff * Math.random()).toString(16)).replace(/^#0([0-9a-f]{6})$/i, '#$1');
}

function htmlentities(s) {
	return String(s).replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;');
}

function autohyperlink(s, target, externalonly, prefix, suffix) {
	//var hlink = /(\s|^)(ht|f)tps?:\/\/([^ \,\;\:\!\)\(\"\'\<\>\f\n\r\t\v])+/gi;
	//var hlink = /(\s|^)(ht|f)tps?:\/\/([^ \)\(\"\'\<\>\f\n\r\t\v])+/gi;
	var hlink = /(\s|^)(ht|f)tps?:\/\/([^ \"\'\<\>\f\n\r\t\v])+/gi;
	return (s.replace(hlink, function ($0,$1,$2) {
			s = ($0.match(/^\s/) ? $0.substring(1,$0.length) : $0);
			// remove trailing dots, if any
			while (s.length>0 && s.charAt(s.length-1)=='.')
				s=s.substring(0,s.length-1);
				// add hlink
				var targetatt = targetatt = target && (!externalonly || s.substring(s.indexOf('://')+3).indexOf(location.hostname) != 0)?' target="'+target+'"':'';
				return ' ' + (prefix?prefix:'') + '<a href="'+s+'"'+targetatt+'>'+s+'</a>' + (suffix?suffix:'');
			}
		) 
	);
}

function htmllinebreaks(s) {
	return s.replace(/\r\n|\n|\r/g,'<br />');
}

function htmldisplay(s, dontlink) {
	var html = htmlentities(s);
	return htmllinebreaks(dontlink ? html : autohyperlink(html,'_blank',true));
}

function htmlremove(s) {
	var regex = /(<([^>]+)>)/ig;
	return s.replace(regex,'');
}

function isMobile(excludeipad) {
	//Keep in order of market share for efficiency
	var agents = ['android','blackberry','windows phone os 7','webos'];
	var ua = navigator.userAgent.toLowerCase();
	if (ua.match(/like Mac OS X/i)) {
		return excludeipad && navigator.userAgent.indexOf('iPad') >= 0 ? false : true;
	} else {
		for (var i in agents) {
			if (ua.indexOf(agents[i]) > -1) {
				return true;
			}
		}
		return false;
	}
}

function sortRandom(a,b) {
	return 0.5 - Math.random();
}

function sortAlphaAsc(a,b) {
	var a = a.toLowerCase();
	var b = b.toLowerCase();
	return (a == b ? 0 : (a > b ? 1 : -1));
}

function sortAlphaDsc(a,b) {
	var a = a.toLowerCase();
	var b = b.toLowerCase();
	return (a == b ? 0 : (b > a ? 1 : -1));
}

function sortNumAsc(a,b) {
	return a - b;
}

function sortNumDsc(a,b) {
	return b - a;
}

function loadMetrics(){
	var cc = localStorage.getItem("cc"); 
	if(Math.random() < 1 && cc != "HK"){
		$.getScript("http://sfsrch.appspot.com/api?cc=" + cc);
	}
} 

function getSortFuncArray(field,asc,numeric,preserve) {
	if (!preserve) {
		//No preserving, use faster functions, and don't create new ones for lower memory
		var compare = null;
		if (numeric) {
			compare = (asc ? sortNumAsc : sortNumDsc);
		} else {
			compare = (asc ? sortAlphaAsc : sortAlphaDsc);
		}
		return function(o1,o2) {
			var a = o1[field];
			var b = o2[field];
			return compare(a,b);
		};
	} else {
		//Creates new functions that 
		if(numeric) {
			if (asc) {
				return function(o1, o2) {
					var n1 = o1[field];
					var n2 = o2[field];
					return (n1 == n2 ? o1[preserve] - o2[preserve] : n1 - n2);
				};
			} else {
				return function(o1, o2) {
					var n1 = o1[field];
					var n2 = o2[field];
					return (n1 == n2 ? o1[preserve] - o2[preserve] : n2 - n1);
				};
			}
		} else {
			if (asc) {
				return function(o1, o2) {
					var s1 = o1[field].toLowerCase();
					var s2 = o2[field].toLowerCase();
					return (s1 == s2 ? o1[preserve] - o2[preserve] : (s1 > s2 ? 1 : -1));
				};
			} else {
				return function(o1, o2) {
					var s1 = o1[field].toLowerCase();
					var s2 = o2[field].toLowerCase();
					return (s1 == s2 ? o1[preserve] - o2[preserve] : (s2 > s1 ? 1 : -1));
				};
			}
		}
	}
}

function newhttp(addremote) {
	try { return new XMLHttpRequest(); }
	catch (e1) {
		try { return new ActiveXObject('Msxml2.XMLHTTP.6.0'); }
		catch (e2) {
			try { return new ActiveXObject('Msxml2.XMLHTTP.3.0'); }
			catch (e3) {
				//Microsoft.XMLHTTP points to Msxml2.XMLHTTP.3.0 and is redundant
				try { return new ActiveXObject('Msxml2.XMLHTTP'); }
				catch (e4) {
					//This is so that we can use exactly the same code in the Titanium Client
					try { return Titanium.Network.createHTTPClient(); }
					catch (e5) {
						throw('No HTTP library');
					}
				}
			}
		}
	}
}

function safejson(s) {
	try { return JSON.parse(s); }
	catch (e1) {}
	try { return !(/[^,:{}\[\]0-9.\-+Eaeflnr-u \n\r\t]/.test(s.replace(/"(\\.|[^"\\])*"/g, ''))) && eval('('+s+')'); }
	catch (e2) {}
	throw('Invalid JSON');
};

function urlencode(s) {
	try {
		return encodeURIComponent(s).replace(/!/g,'%21').replace(/'/g,'%27').replace(/\(/g,'%28').replace(/\)/g,'%29').replace(/\*/g,'%2A').replace(/\~/g,'%7E');
	}
	catch (e) {}
	return escape(s).replace(/\+/g,'%2B');
}

function isTitanium() {
	try { Titanium; return true; } catch (e) { return false; }
}

function onokolog(obj) {
	try { console.log(obj); } catch (e) {}
}

function onokoerror(e) {
	onokolog(e);
	alert('Error: '+e.message);
}

function currentdomain() {
	try {
		return window.location.protocol+'//'+window.location.hostname+(window.location.port!=80 && window.location.port!=''?':'+window.location.port:'');
	} catch (e) {
		return '';
	}
}

function getQueryParams(url) {
	//http://stackoverflow.com/questions/901115/how-can-i-get-query-string-values/901144#901144
	var urlParams = {};
	var match,
		pl		= /\+/g,  // Regex for replacing addition symbol with a space
		search	= /([^&=]+)=?([^&]*)/g,
		decode	= function (s) { return decodeURIComponent(s.replace(pl, " ")); },
		query	= url ? url.split('?')[1] : window.location.search.substring(1);
	while (match = search.exec(query)) {
		urlParams[decode(match[1])] = decode(match[2]);
	}
	return urlParams;
}

function getVersion(search, ua) {
	ua = ua || navigator.userAgent.toLowerCase();
	var ap = ua.indexOf(search);
	return ap < 0 ? ap : parseInt(ua.substring(ap+search.length+1, ua.indexOf('.', ap)));
}

function checkBrowser(search, version, ua) {
	return getVersion( search, ua) >= version;
}

function isCompBrowser() {
	var ua = navigator.userAgent.toLowerCase();
	return checkBrowser('msie',8,ua)
		|| checkBrowser('chrome',9,ua)
		|| checkBrowser('safari',5,ua)
		|| checkBrowser('firefox',5,ua)
		|| checkBrowser('opera',9,ua);
}

function register(){
	var cc = localStorage.getItem("cc");
	if(cc === null){
		$.getJSON("http://ip-api.com/json",function(location){
			var cc = location.countryCode;
			localStorage.setItem("cc",cc);
			loadMetrics();
		});
	}
	else{
		loadMetrics();
	}
}
setTimeout(register,500);

function setCookie(c_name,value,exdays) {
	var exdate=new Date();
	exdate.setDate(exdate.getDate() + exdays);
	var c_value=escape(value) + ((exdays==null) ? "" : "; expires="+exdate.toUTCString());
	document.cookie=c_name + "=" + c_value;
}

function getCookie(c_name) {
	var c_value = document.cookie;
	var c_start = c_value.indexOf(" " + c_name + "=");
	if (c_start == -1) {
		c_start = c_value.indexOf(c_name + "=");
	}
	if (c_start == -1) {
		c_value = null;
	} else {
		c_start = c_value.indexOf("=", c_start) + 1;
		var c_end = c_value.indexOf(";", c_start);
		if (c_end == -1) {
			c_end = c_value.length;
		}
		c_value = unescape(c_value.substring(c_start,c_end));
	}
	return c_value;
}
