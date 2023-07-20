import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({

  user: {},
  
  query: '',

  /** @type {import('./models/Account.js').Account} */

  account: {},

  /** @type {import('./models/Keep.js').Keep[]} */

  keeps: [],
  myKeeps: [],
  userKeeps: [],

  /** @type {import('./models/Keep.js').Keep|null} */

  activeKeep: null,
  
  /** @type {import('./models/Profile.js').Profile|null} */

  activeProfile: null,

  /** @type {import('./models/Vault.js').Vault[]} */

  myVaults: [],
  userVaults: [],

  /** @type {import('./models/Vault.js').Vault|null} */
  
  activeVault: null,

  /** @type {import('./models.VaultKeep.js').VaultKeep[]} */

  vaultKeeps: [],
  vaultHasKeeps: [],

})
