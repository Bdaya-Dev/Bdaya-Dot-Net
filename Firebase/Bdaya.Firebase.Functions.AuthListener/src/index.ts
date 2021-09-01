import * as functions from "firebase-functions";
import * as admin from "firebase-admin";

let initialized = false;
let db: FirebaseFirestore.Firestore;

function initialize() {
    if (initialized === true) return;
    initialized = true;
    admin.initializeApp();
    db = admin.firestore();
}
initialize();
export const syncFirestoreCreate = functions.auth.user().onCreate(async (user) => {
    initialize();
    
    const userJson: any = user.toJSON();
    const providers: Array<any> | null | undefined = userJson.providerData;
    if (providers != null) {
        for (const p of providers) {
            delete p.toJSON;
        }
    }
    await admin.firestore().collection("users").doc(user.uid).create(userJson);
});

export const syncFirestoreDelete = functions.auth.user().onDelete(async (user) => {
    initialize();
    await db.collection("users").doc(user.uid).delete();
});
