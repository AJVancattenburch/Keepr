import { AppState } from "../AppState.js"
import { VaultHasKeep } from "../models/VaultKeep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {
  
  async getKeepsByVaultId(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    AppState.vaultHasKeeps = res.data.map(vhk => new VaultHasKeep(vhk))
  }

  async addKeepToVault(vaultId, keepId) {
    const newVaultKeep = {vaultId: vaultId, keepId: keepId }
    const res = await api.post(`api/vaultkeeps`, newVaultKeep)
    logger.log('[KEEP ADDED TO VAULT] =>', res.data)
    AppState.activeKeep.kept++
    logger.log('[KEPT COUNT] =>', AppState.activeKeep.kept)
  }

  async removeKeepFromVault(vaultKeepId) {
    const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    const removedFromVault = AppState.vaultKeeps.findIndex(k => k.vaultKeepId == vaultKeepId)
    AppState.vaultKeeps.splice(removedFromVault, 1)
  }
}

export const vaultKeepsService = new VaultKeepsService()