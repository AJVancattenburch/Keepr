import { AppState } from '../AppState'
import { Vault } from '../models/Vault.js'
import { logger } from '../utils/Logger.js'
import { api } from './AxiosService.js'

class VaultsService {

  async createVault(newVault) {
    const res = await api.post('api/vaults', newVault)
    AppState.myVaults.unshift(new Vault(res.data))
    logger.log('[NEW VAULT CREATED] =>', res.data)
  }
}

export const vaultsService = new VaultsService()