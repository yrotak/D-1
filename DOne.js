function str2Arr(str) {
    var result = [];
    for (let i = 0; i < str.length; i++) {
        result.push(str.charCodeAt(i))
    }
    return result;
}
function arr2str(arr) {
    var result = "";
    arr.forEach(charcode => {
        result+=String.fromCharCode(charcode);
    });
    return result;
}
var alphabetBaseMain = str2Arr("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
const DOne = {
    encrypt: function (key, plain) {
        if (typeof plain != 'object')
            throw new Error("Plain type must be a Buffer")
        var result = [];
        var alphabetBase1 = DOne_CreateAlphabetBase(plain);
        alphabetBaseMain = alphabetBase1.alphabet;
        var rest = alphabetBase1.rest;
        for (let i = 0; i < str2Arr(key).length; i++) {
            if (![...alphabetBaseMain].includes(str2Arr(key)[i]))
                throw new Error("Key contain byte that are not in alphabetBase")
        }
        var formatedkey = DOne_FormatKey(key);
        var alphabetList = [];
        for (let i = 0; i < formatedkey.length; i++)
            alphabetList.push(DOne_CreateAlphabet(formatedkey[i]));

        for (let i = 0, o = 0; i < plain.length; i++, o++) {
            var at = alphabetBaseMain.indexOf(plain[i]);
            if (i >= 32)
                o = 0;
            result.push(alphabetList[o][at]);
        }
        return btoa(arr2str(result)) + (rest == "" ? "" : ".") + rest;
    },
    decrypt: function (key, encrypted_b64) {
        var rest = "";
        var encrypted = null;
        if (encrypted_b64.split('.').length == 2) {
            rest = encrypted_b64.split('.')[1];
            encrypted = str2Arr(atob(encrypted_b64.split('.')[0]));
        } else {
            encrypted = str2Arr(atob(encrypted_b64));
        }
        var result = [];
        alphabetBaseMain = DOne_CreateAlphabetBaseFromRest(rest).alphabet;
        for (let i = 0; i < str2Arr(key).length; i++) {
            if (![...alphabetBaseMain].includes(str2Arr(key)[i]))
                throw new Error("Key contain byte that are not in alphabetBase")
        }
        var formatedkey = DOne_FormatKey(key);
        var alphabetList = [];
        for (let i = 0; i < formatedkey.length; i++)
            alphabetList.push(DOne_CreateAlphabet(formatedkey[i]));

        for (let i = 0, o = 0; i < encrypted.length; i++, o++) {
            if (i >= 32)
                o = 0;
            var at = alphabetList[o].indexOf(encrypted[i]);
            result.push(alphabetBaseMain[at]);
        }
        return arr2str(result);
    }
};

function DOne_FormatKey(key) {
    var result2 = [];
    for (let i = 0; result2.length < 32; i++) {
        for (let keyI = 0; keyI < key.length; keyI++) {
            result2.push(str2Arr(key)[keyI]);
        }
        result2 = result2.reverse();
    }
    return result2.slice(0, 32);
}
function DOne_CreateAlphabetBase(plain) {
    var rest = "";
    for (let i = 0; i < plain.length; i++) {
        if (!alphabetBaseMain.includes(plain[i])) {
            rest += String.fromCharCode(plain[i]);
            alphabetBaseMain.push(plain[i]);
        }
    }
    return {
        alphabet: alphabetBaseMain,
        rest: btoa(rest)
    };
}
function DOne_CreateAlphabetBaseFromRest(rest) {
    for (let i = 0; i < str2Arr(atob(rest)).length; i++) {
        if (!alphabetBaseMain.includes(str2Arr(atob(rest))[i])) {
            alphabetBaseMain = alphabetBaseMain.concat(str2Arr(atob(rest))[i]);
        }
    }
    return {
        alphabet: alphabetBaseMain,
        rest: ""
    };
}
function DOne_CreateAlphabet(keybyte) {
    var result = [...alphabetBaseMain];
    var result2 = result.splice(alphabetBaseMain.indexOf(keybyte), result.length - 1);
    // console.log(Uint16Array.from(result.splice(alphabetBaseMain.indexOf(keybyte), result.length-1)).toString());
    return result2.concat(result)
}