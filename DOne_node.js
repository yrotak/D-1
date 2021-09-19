var alphabetBaseMain = Buffer.from("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
const DOne = {
    encrypt: function (key, plain) {
        if (!Buffer.isBuffer(plain))
            throw new Error("Plain type must be a Buffer")
        var result = [];
        var alphabetBase1 = DOne_CreateAlphabetBase(plain);
        alphabetBaseMain = alphabetBase1.alphabet;
        var rest = alphabetBase1.rest;
        for (let i = 0; i < Buffer.from(key).length; i++) {
            if (![...alphabetBaseMain].includes(Buffer.from(key)[i]))
                throw new Error("Key contain byte that are not in alphabetBase")
        }
        var formatedkey = DOne_FormatKey(key);
        var alphabetList = [];
        for (let i = 0; i < formatedkey.length; i++)
            alphabetList.push(DOne_CreateAlphabet(formatedkey[i]));

        for (let i = 0, o = 0; i < Buffer.from(plain).length; i++, o++) {
            var at = alphabetBaseMain.indexOf(Buffer.from(plain)[i]);
            if (i >= 32)
                o = 0;
            if (alphabetList[o][at] == undefined) {
                at++
                console.log(alphabetList[o][at], at, alphabetList[o].length);
            }
            result += Buffer.from(alphabetList[o][at]);
        }
        return Buffer.from(result).toString('base64') + (rest == "" ? "" : ".") + rest;
    },
    decrypt: function (key, encrypted_b64) {
        var rest = "";
        var encrypted = null;
        if (encrypted_b64.split('.').length == 2) {
            rest = encrypted_b64.split('.')[1];
            encrypted = Buffer.from(encrypted_b64.split('.')[0], 'base64');
        } else {
            encrypted = Buffer.from(encrypted_b64, 'base64');
        }
        var result = [];
        alphabetBaseMain = DOne_CreateAlphabetBaseFromRest(rest).alphabet;
        for (let i = 0; i < Buffer.from(key).length; i++) {
            if (![...alphabetBaseMain].includes(Buffer.from(key)[i]))
                throw new Error("Key contain byte that are not in alphabetBase")
        }
        var formatedkey = DOne_FormatKey(key);
        var alphabetList = [];
        for (let i = 0; i < formatedkey.length; i++)
            alphabetList.push(DOne_CreateAlphabet(formatedkey[i]));

        for (let i = 0, o = 0; i < Buffer.from(encrypted).length; i++, o++) {
            if (i >= 32)
                o = 0;
            var at = Buffer.from(alphabetList[o]).indexOf(encrypted[i]);
            result += Buffer.from([alphabetBaseMain[at]]);
        }
        return result;
    }
};

function DOne_FormatKey(key) {
    var result2 = [];
    for (let i = 0; result2.length < 32; i++) {
        for (let keyI = 0; keyI < key.length; keyI++) {
            result2.push(Buffer.from(key)[keyI]);
        }
        result2 = result2.reverse();
    }
    return Buffer.from(result2.slice(0, 32));
}
function DOne_CreateAlphabetBase(plain) {
    var rest = "";
    for (let i = 0; i < plain.length; i++) {
        if (!alphabetBaseMain.includes(plain[i])) {
            rest += Buffer.from([plain[i]]).toString();
            alphabetBaseMain = Buffer.concat([alphabetBaseMain, Buffer.from([plain[i]])]);
        }
    }
    return {
        alphabet: alphabetBaseMain,
        rest: Buffer.from(rest).toString('base64')
    };
}
function DOne_CreateAlphabetBaseFromRest(rest) {
    for (let i = 0; i < Buffer.from(rest, 'base64').length; i++) {
        if (!alphabetBaseMain.includes(Buffer.from(rest, 'base64')[i])) {
            alphabetBaseMain = Buffer.concat([alphabetBaseMain, Buffer.from([Buffer.from(rest, 'base64')[i]])]);
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
    // console.log(Buffer.from(result.splice(alphabetBaseMain.indexOf(keybyte), result.length-1)).toString());
    return Buffer.from(result2) + Buffer.from(result)
}
module.exports = DOne;